using DependencyInjection.Sample.LooselyCoupled.Application.Interceptors;
using DependencyInjection.Sample.LooselyCoupled.Core;
using DependencyInjection.Sample.LooselyCoupled.Core.DataAccess;
using DependencyInjection.Sample.LooselyCoupled.Core.DataAccess.CosmosDb;
using DependencyInjection.Sample.LooselyCoupled.Core.Discounts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample.LooselyCoupled.Application
{
    internal class ManualCompositionRoot
    {
        public ListProductsUi ProductsUi { get; private set; }

        public async Task<ManualCompositionRoot> ConfigureServices()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("*", LogLevel.Debug)
                    .AddConsole();
            });

            var dbEndpoint = configuration["DbEndpoint"];
            var dbAccessKey = configuration["DbAccessKey"];
            var cosmosClientInitializer = new CosmosClientInitializer(dbEndpoint, dbAccessKey);
            var cosmosDbClient = await cosmosClientInitializer.CreateAndInitializeAsync(new List<(string, string)>
            {
                ("DependencyInjectionSample", "Products")
            });
            IProductRepository productRepository = new CosmosProductRepository(cosmosDbClient);
            if (IsDebugRun(configuration))
            {
                var logger = loggerFactory.CreateLogger<ProductRepositoryLoggingInterceptor>();
                productRepository = new ProductRepositoryLoggingInterceptor(productRepository, logger);
            }
            var discountPolicyProvider = new DefaultDiscountPolicyProvider();
            var userContextAccessor = new LoggedInUserAccessor();
            var productService = new ProductService(productRepository, discountPolicyProvider, userContextAccessor);

            // create UI service
            ProductsUi = new ListProductsUi(productService);

            return this;
        }

        private static bool IsDebugRun(IConfigurationRoot configuration)
        {
            return bool.TryParse(configuration["DebugRun"], out var debugRun) && debugRun;
        }
    }
}
