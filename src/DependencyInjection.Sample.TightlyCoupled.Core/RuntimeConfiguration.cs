using Microsoft.Extensions.Configuration;

namespace DependencyInjection.Sample.TightlyCoupled.Core
{
    public class RuntimeConfiguration
    {
        private readonly string _dbEndpoint;
        private readonly string _dbAccessKey;

        private static RuntimeConfiguration _instance;
        
        public static void Initialize(IConfiguration settings)
        {
            _instance = new RuntimeConfiguration(settings);
        }

        public static string DbEndpoint => _instance._dbEndpoint;

        public static string DbAccessKey => _instance._dbAccessKey;

        private RuntimeConfiguration(IConfiguration settings)
        {
            _dbEndpoint = settings["DbEndpoint"];
            _dbAccessKey = settings["DbAccessKey"];
        }
    }
}
