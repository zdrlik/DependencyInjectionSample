namespace DependencyInjection.Sample.LooselyCoupled.Core.Discounts
{
    public interface IDiscountPolicy
    {
        public Product ApplyDiscount(Product product);
    }
}
