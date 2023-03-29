namespace DependencyInjection.Sample.LooselyCoupled.Core.DataAccess
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
