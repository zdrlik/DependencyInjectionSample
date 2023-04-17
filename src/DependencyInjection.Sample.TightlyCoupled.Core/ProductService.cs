using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample.TightlyCoupled.Core
{
    public class ProductService
    {
        private readonly CosmosProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService()
        {
            _productRepository = new CosmosProductRepository();
            _logger = LogManager.GetLogger<ProductService>();
        }
        
        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            var result = await _productRepository.GetProducts();

            var discount = UserContext.GetCurrentUser().IsPreferredCustomer ? 0.95m : 1;
            ApplyDiscount(result, discount);

            _logger.LogDebug($"Returned {result.Count} items.");

            return result;
        }

        private void ApplyDiscount(IReadOnlyList<Product> result, decimal discount)
        {
            foreach (var item in result)
            {
                item.UnitPrice *= discount;
            }
        }
    }
}