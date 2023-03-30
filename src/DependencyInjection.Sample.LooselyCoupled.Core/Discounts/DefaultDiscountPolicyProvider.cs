namespace DependencyInjection.Sample.LooselyCoupled.Core.Discounts
{
    public class DefaultDiscountPolicyProvider : IDiscountPolicyProvider
    {
        private readonly decimal _preferredCustomerDiscount;

        private readonly IDiscountPolicy _preferredCustomerDiscountPolicy;
        private readonly IDiscountPolicy _noDiscountPolicy;

        public DefaultDiscountPolicyProvider(decimal preferredCustomerDiscount = 0.95m)
        {
            if (preferredCustomerDiscount <= 0.1m)
                throw new ArgumentOutOfRangeException(nameof(preferredCustomerDiscount));

            _preferredCustomerDiscountPolicy = new PreferredCustomerDiscountPolicy(preferredCustomerDiscount);
            _noDiscountPolicy = new NoDiscountPolicy();
        }

        public IDiscountPolicy GetDiscountPolicy(User forUser)
        {
            if (forUser == null) throw new ArgumentNullException(nameof(forUser));

            return forUser.IsPreferredCustomer ? _preferredCustomerDiscountPolicy : _noDiscountPolicy;
        }
    }
}
