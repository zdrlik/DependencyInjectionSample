﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjection.Sample.LooselyCoupled.Application.Interceptors;
using DependencyInjection.Sample.LooselyCoupled.Core;
using DependencyInjection.Sample.LooselyCoupled.Core.DataAccess;
using DependencyInjection.Sample.LooselyCoupled.Core.DataAccess.CosmosDb;
using DependencyInjection.Sample.LooselyCoupled.Core.Discounts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample.LooselyCoupled.Application
{
    internal class ServiceCollectionCompositionRoot
    {
        public ListProductsUi ProductsUi { get; private set; }

        public async Task<ServiceCollectionCompositionRoot> ConfigureServices()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddFilter("*", LogLevel.Debug)
                .AddConsole());
            serviceCollection.AddSingleton<IDiscountPolicyProvider, DefaultDiscountPolicyProvider>();
            serviceCollection.AddSingleton<IUserContextAccessor, LoggedInUserAccessor>();
            serviceCollection.AddSingleton<IProductService, ProductService>();
            serviceCollection.AddSingleton<ListProductsUi>();

            var dbEndpoint = configuration["DbEndpoint"];
            var dbAccessKey = configuration["DbAccessKey"];
            var cosmosClientInitializer = new CosmosClientInitializer(dbEndpoint, dbAccessKey);
            var cosmosDbClient = await cosmosClientInitializer.CreateAndInitializeAsync(new List<(string, string)>
            {
                ("DependencyInjectionSample", "Products")
            });
            serviceCollection.AddSingleton(cosmosDbClient);

            serviceCollection.AddSingleton<IProductRepository>(factory =>
            {
                IProductRepository productRepository = new CosmosProductRepository(cosmosDbClient);
                if (IsDebugRun(configuration))
                {
                    var loggerFactory = factory.GetService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger<ProductRepositoryLoggingInterceptor>();
                    return new ProductRepositoryLoggingInterceptor(productRepository, logger);
                }
                else
                {
                    return productRepository;
                }
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            this.ProductsUi = serviceProvider.GetService<ListProductsUi>(); 
            return this;
        }

        private static bool IsDebugRun(IConfigurationRoot configuration)
        {
            return bool.TryParse(configuration["DebugRun"], out var debugRun) && debugRun;
        }
    }
}
