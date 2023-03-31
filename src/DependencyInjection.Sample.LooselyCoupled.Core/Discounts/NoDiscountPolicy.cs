using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample.LooselyCoupled.Core.Discounts
{
    internal class NoDiscountPolicy : IDiscountPolicy
    {
        private readonly ILogger<NoDiscountPolicy> _logger;

        public NoDiscountPolicy(ILogger<NoDiscountPolicy> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<Product> ApplyDiscount(IEnumerable<Product> products)
        {
            _logger.LogDebug("No discount applied");

            return products;
        }
    }
}
