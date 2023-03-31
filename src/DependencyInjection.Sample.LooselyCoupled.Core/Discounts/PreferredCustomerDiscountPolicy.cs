using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample.LooselyCoupled.Core.Discounts
{
    internal class PreferredCustomerDiscountPolicy : IDiscountPolicy
    {
        private readonly decimal _discount;
        private readonly ILogger<PreferredCustomerDiscountPolicy> _logger;

        public PreferredCustomerDiscountPolicy(decimal discount, ILogger<PreferredCustomerDiscountPolicy> logger)
        {
            if (discount <= 0.1m) throw new ArgumentOutOfRangeException(nameof(discount));

            _discount = discount;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<Product> ApplyDiscount(IEnumerable<Product> products)
        {
            _logger.LogDebug($"Applied {_discount} discount");

            return products.Select(ApplyDiscount);
        }

        private Product ApplyDiscount(Product product)
        {
            product.UnitPrice *= _discount;
            return product;
        }
    }
}
