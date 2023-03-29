namespace DependencyInjection.Sample.LooselyCoupled.Core.Discounts
{
    internal class NoDiscountPolicy : IDiscountPolicy
    {
        public Product ApplyDiscount(Product product)
        {
            return product;
        }
    }
}
