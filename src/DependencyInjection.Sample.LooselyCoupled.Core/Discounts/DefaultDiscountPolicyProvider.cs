using Microsoft.Extensions.Logging;

namespace DependencyInjection.Sample.LooselyCoupled.Core.Discounts
{
    public class DefaultDiscountPolicyProvider : IDiscountPolicyProvider
    {
        private readonly IDiscountPolicy _preferredCustomerDiscountPolicy;
        private readonly IDiscountPolicy _noDiscountPolicy;

        public DefaultDiscountPolicyProvider(ILoggerFactory loggerFactory, decimal preferredCustomerDiscount = 0.95m)
        {
            if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));
            if (preferredCustomerDiscount <= 0.1m)
                throw new ArgumentOutOfRangeException(nameof(preferredCustomerDiscount));

            _preferredCustomerDiscountPolicy = new PreferredCustomerDiscountPolicy(preferredCustomerDiscount, loggerFactory.CreateLogger<PreferredCustomerDiscountPolicy>());
            _noDiscountPolicy = new NoDiscountPolicy(loggerFactory.CreateLogger<NoDiscountPolicy>());
        }

        public IDiscountPolicy GetDiscountPolicy(User forUser)
        {
            if (forUser == null) throw new ArgumentNullException(nameof(forUser));

            return forUser.IsPreferredCustomer ? _preferredCustomerDiscountPolicy : _noDiscountPolicy;
        }
    }
}
