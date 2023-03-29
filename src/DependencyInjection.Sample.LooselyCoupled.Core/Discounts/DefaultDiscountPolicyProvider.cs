namespace DependencyInjection.Sample.LooselyCoupled.Core.Discounts
{
    public class DefaultDiscountPolicyProvider : IDiscountPolicyProvider
    {

        private readonly IDiscountPolicy _preferredCustomerDiscountPolicy = new PreferredCustomerDiscountPolicy(0.95m);
        private readonly IDiscountPolicy _noDiscountPolicy = new NoDiscountPolicy();

        public DefaultDiscountPolicyProvider()
        {
        }

        public IDiscountPolicy GetDiscountPolicy(User forUser)
        {
            if (forUser == null) throw new ArgumentNullException(nameof(forUser));

            return forUser.IsPreferredCustomer ? _preferredCustomerDiscountPolicy : _noDiscountPolicy;
        }
    }
}
