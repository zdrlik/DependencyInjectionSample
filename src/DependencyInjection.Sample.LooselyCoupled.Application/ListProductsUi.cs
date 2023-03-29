using DependencyInjection.Sample.LooselyCoupled.Core;

namespace DependencyInjection.Sample.LooselyCoupled.Application
{
    public interface IListProductsUi
    {
        Task ShowProducts();
    }

    public class ListProductsUi : IListProductsUi
    {
        private readonly IProductService _productService;

        public ListProductsUi(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task ShowProducts()
        {
            var featuredProducts = await _productService.GetProducts();
            Console.WriteLine("Discounted products:");
            featuredProducts.ToList().ForEach(p => Console.WriteLine($"{p.Name}: {p.UnitPrice:C}"));
        }
    }
}
