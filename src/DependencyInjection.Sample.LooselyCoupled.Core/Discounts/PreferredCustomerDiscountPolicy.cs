namespace DependencyInjection.Sample.LooselyCoupled.Core.Discounts
{
    internal class PreferredCustomerDiscountPolicy : IDiscountPolicy
    {
        private readonly decimal _discountValue;

        public PreferredCustomerDiscountPolicy(decimal discountValue)
        {
            _discountValue = discountValue;
        }

        public Product ApplyDiscount(Product product)
        {
            return product with { UnitPrice = product.UnitPrice * _discountValue };
        }
    }
}
