using Microsoft.Azure.Cosmos;

namespace DependencyInjection.Sample.TightlyCoupled.Core
{
    public class ProductService
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public ProductService()
        {
            var endpoint = RuntimeConfiguration.DbEndpoint;
            var accessKey = RuntimeConfiguration.DbAccessKey;
            
            _cosmosClient = CreateAndInitializeDbClient(endpoint, accessKey).GetAwaiter().GetResult();
            _container = _cosmosClient.GetContainer("DependencyInjectionSample", "Products");
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

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            var iterator = _container.GetItemQueryIterator<Product>("select * from c");

            var result = new List<Product>();
            while (iterator.HasMoreResults)
            {
                var item = await iterator.ReadNextAsync();
                result.AddRange(item);
            }

            var discount = UserContext.GetCurrentUser().IsPreferredCustomer ? 0.95m : 1;
            ApplyDiscount(result, discount);

            return result;
        }

        private void ApplyDiscount(List<Product> result, decimal discount)
        {
            result.ForEach(p => p.UnitPrice *= discount);
        }
    }
}