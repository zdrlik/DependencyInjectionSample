using Microsoft.Azure.Cosmos;

namespace DependencyInjection.Sample.LooselyCoupled.Core.DataAccess.CosmosDb
{
    public class CosmosClientInitializer : ICosmosClientInitializer
    {
        private readonly string _endpoint;
        private readonly string _accessKey;

        public CosmosClientInitializer(string endpoint, string accessKey)
        {
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            _accessKey = accessKey ?? throw new ArgumentNullException(nameof(accessKey));
        }

        public async Task<CosmosClient> CreateAndInitializeAsync(List<(string, string)> containers)
        {
            var cosmosClientOptions = new CosmosClientOptions
            {
                ConnectionMode = ConnectionMode.Direct
            };

            var cosmosClient = await CosmosClient.CreateAndInitializeAsync(_endpoint, _accessKey, containers, cosmosClientOptions);
            return cosmosClient;
        }
    }
}
