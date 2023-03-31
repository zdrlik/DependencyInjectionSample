namespace DependencyInjection.Sample.LooselyCoupled.Core.DataAccess
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();
    }
}
