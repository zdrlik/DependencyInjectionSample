using Microsoft.Azure.Cosmos;

namespace DependencyInjection.Sample.LooselyCoupled.Core.DataAccess.CosmosDb;

internal interface ICosmosClientInitializer
{
    Task<CosmosClient> CreateAndInitializeAsync(List<(string, string)> containers);
}