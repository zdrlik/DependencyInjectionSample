using System;
using System.Threading.Tasks;
using DependencyInjection.Sample.TightlyCoupled.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample.TightlyCoupled.Application
{
    internal class Program
    {
        static async Task Main()
        {
            ConfigureAppSettings();
            ConfigureLogging();

            var productsUi = new ListProductsUi();
            await productsUi.ShowProducts();

            Console.WriteLine();
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }

        private static void ConfigureAppSettings()
        {
            RuntimeConfiguration.Initialize(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build());
        }

        private static void ConfigureLogging()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("*", LogLevel.Debug)
                    .AddConsole();
            });

            LogManager.LoggerFactory = loggerFactory;
        }
    }
}