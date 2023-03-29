using DependencyInjection.Sample.TightlyCoupled.Core;

namespace DependencyInjection.Sample.TightlyCoupled.Application
{
    internal class ListProductsUi
    {
        private readonly ProductService _productService;

        public ListProductsUi()
        {
            _productService = new ProductService();
        }

        public async Task ShowProducts()
        {
            var featuredProducts = await _productService.GetProducts();
            Console.WriteLine("Discounted products:");
            featuredProducts.ToList().ForEach(p => Console.WriteLine($"{p.Name}: {p.UnitPrice:C}"));
        }
    }
}
