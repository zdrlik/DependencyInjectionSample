using DependencyInjection.Sample.LooselyCoupled.Core;
using DependencyInjection.Sample.LooselyCoupled.Core.DataAccess;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample.LooselyCoupled.Application.Interceptors
{
    internal class ProductRepositoryLoggingInterceptor : IProductRepository
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductRepositoryLoggingInterceptor> _logger;

        public ProductRepositoryLoggingInterceptor(IProductRepository productRepository, ILogger<ProductRepositoryLoggingInterceptor> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            var result = await _productRepository.GetProductsAsync();
            
            _logger.LogDebug($"Retrieved {result.Count} products");
            
            return result;
        }
    }
}
