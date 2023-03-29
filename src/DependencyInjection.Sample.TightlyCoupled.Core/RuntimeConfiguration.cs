using Microsoft.Extensions.Configuration;

namespace DependencyInjection.Sample.TightlyCoupled.Core
{
    public static class RuntimeConfiguration
    {
        public static IConfiguration AppSettings { get; }
        public static string DbEndpoint { get; set; }
        public static string DbAccessKey { get; set; }

        static RuntimeConfiguration()
        {
            AppSettings = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DbEndpoint = AppSettings["DbEndpoint"];
            DbAccessKey = AppSettings["DbAccessKey"];
        }
    }
}
