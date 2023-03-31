using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample.TightlyCoupled.Core
{
    internal class CosmosProductRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;
        private readonly ILogger<CosmosProductRepository> _logger;

        public CosmosProductRepository()
        {
            var endpoint = RuntimeConfiguration.DbEndpoint;
            var accessKey = RuntimeConfiguration.DbAccessKey;

            _cosmosClient = CreateAndInitializeDbClient(endpoint, accessKey).GetAwaiter().GetResult();
            _container = _cosmosClient.GetContainer("DependencyInjectionSample", "Products");

            _logger = LogManager.GetLogger<CosmosProductRepository>();
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            var iterator = _container.GetItemQueryIterator<Product>("select * from c");

            var result = new List<Product>();
            while (iterator.HasMoreResults)
            {
                var item = await iterator.ReadNextAsync();
                result.AddRange(item);
            }

            _logger.LogDebug($"Retrieved {result.Count} products");

            return result;
        }

        private async Task<CosmosClient> CreateAndInitializeDbClient(string endpoint, string accessKey)
        {
            var containers = new List<(string, string)>
            {
                ("DependencyInjectionSample", "Products")
            };
            var cosmosClientOptions = new CosmosClientOptions
            {
                ConnectionMode = ConnectionMode.Direct
            };

            var cosmosClient = await CosmosClient.CreateAndInitializeAsync(endpoint, accessKey, containers, cosmosClientOptions);
            return cosmosClient;
        }

    }
}
