using DependencyInjection.Sample.LooselyCoupled.Core.DataAccess;
using DependencyInjection.Sample.LooselyCoupled.Core.Discounts;

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

        public ProductService(IProductRepository productRepository, IDiscountPolicyProvider discountPolicyProvider, IUserContextAccessor userContextAccessor)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _discountPolicyProvider = discountPolicyProvider ?? throw new ArgumentNullException(nameof(discountPolicyProvider));
            _userContextAccessor = userContextAccessor ?? throw new ArgumentNullException(nameof(userContextAccessor));
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            var currentUser = _userContextAccessor.GetCurrentUser();
            var discountPolicy = _discountPolicyProvider.GetDiscountPolicy(currentUser);

            var products = await _productRepository.GetProductsAsync();

            return discountPolicy.ApplyDiscount(products).ToList();
        }
    }
}
