using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DependencyInjection.Sample.LooselyCoupled.Core.DataAccess.CosmosDb
{
    public class CosmosProductRepository : IProductRepository
    {
        private readonly Container _container;

        public CosmosProductRepository(CosmosClient cosmosClient)
        {
            if (cosmosClient == null) throw new ArgumentNullException(nameof(cosmosClient));
            _container = cosmosClient.GetContainer("DependencyInjectionSample", "Products");
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            var query = "select * from c";
            var iterator = _container.GetItemQueryIterator<ProductEntity>(query);
            return await ReadProducts(iterator);
        }

        private async Task<IReadOnlyList<Product>> ReadProducts(FeedIterator<ProductEntity> iterator)
        {
            var result = new List<Product>();
            while (iterator.HasMoreResults)
            {
                var item = await iterator.ReadNextAsync();
                result.AddRange(item.Select(p => new Product()
                {
                    Id = p.Id,
                    Name = p.Name,
                    UnitPrice = p.UnitPrice,
                    Description = p.Name
                }));
            }
            return result;
        }

        private record ProductEntity
        {
            [JsonProperty("id")]
            public Guid Id { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("description")]
            public string Description { get; set; }
            [JsonProperty("unit_price")]
            public decimal UnitPrice { get; set; }
        }
    }

}
