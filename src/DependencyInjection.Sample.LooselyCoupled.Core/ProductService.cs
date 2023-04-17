using DependencyInjection.Sample.LooselyCoupled.Core.DataAccess;
using DependencyInjection.Sample.LooselyCoupled.Core.Discounts;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.RecordIO;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample.LooselyCoupled.Core
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetProducts();
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDiscountPolicyProvider _discountPolicyProvider;
        private readonly IUserContextAccessor _userContextAccessor;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, 
            IDiscountPolicyProvider discountPolicyProvider, 
            IUserContextAccessor userContextAccessor, ILogger<ProductService> logger)
        {
            _productRepository = productRepository 
                                 ?? throw new ArgumentNullException(nameof(productRepository));
            _discountPolicyProvider = discountPolicyProvider 
                                      ?? throw new ArgumentNullException(nameof(discountPolicyProvider));
            _userContextAccessor = userContextAccessor 
                                   ?? throw new ArgumentNullException(nameof(userContextAccessor));
            _logger = logger 
                      ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();

            var currentUser = _userContextAccessor.GetCurrentUser();
            var discountPolicy = _discountPolicyProvider.GetDiscountPolicy(currentUser);

            var result = discountPolicy.ApplyDiscount(products).ToList();
            _logger.LogDebug($"Returned {result.Count} items.");
            return result;
        }
    }
}
