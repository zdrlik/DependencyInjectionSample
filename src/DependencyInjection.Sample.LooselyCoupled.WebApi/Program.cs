using DependencyInjection.Sample.LooselyCoupled.Core.Discounts;
using DependencyInjection.Sample.LooselyCoupled.Core;
using DependencyInjection.Sample.LooselyCoupled.Core.DataAccess;
using DependencyInjection.Sample.LooselyCoupled.Core.DataAccess.CosmosDb;
using Microsoft.Azure.Cosmos;

namespace DependencyInjection.Sample.LooselyCoupled.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            await ConfigureServices(builder.Services);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        public static async Task ConfigureServices(IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            serviceCollection.AddLogging(builder => builder
                .AddFilter("*", LogLevel.Debug)
                .AddConsole());
            serviceCollection.AddHttpContextAccessor();
            serviceCollection.AddSingleton<IDiscountPolicyProvider, DefaultDiscountPolicyProvider>();
            serviceCollection.AddScoped<IUserContextAccessor, HttpContextUserAccessor>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddSingleton<IProductRepository, CosmosProductRepository>();

            var cosmosDbClient = await AddCosmosDbClient(configuration, serviceCollection);
            
        }

        private static async Task<CosmosClient> AddCosmosDbClient(IConfigurationRoot configuration, IServiceCollection serviceCollection)
        {
            var dbEndpoint = configuration["DbEndpoint"];
            var dbAccessKey = configuration["DbAccessKey"];
            var cosmosClientInitializer = new CosmosClientInitializer(dbEndpoint, dbAccessKey);
            var cosmosDbClient = await cosmosClientInitializer.CreateAndInitializeAsync(new List<(string, string)>
            {
                ("DependencyInjectionSample", "Products")
            });
            serviceCollection.AddSingleton(cosmosDbClient);
            return cosmosDbClient;
        }

        private static bool IsDebugRun(IConfigurationRoot configuration)
        {
            return bool.TryParse(configuration["DebugRun"], out var debugRun) && debugRun;
        }
    }
}
