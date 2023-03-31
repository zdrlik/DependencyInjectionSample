namespace DependencyInjection.Sample.LooselyCoupled.Core.Discounts
{
    public interface IDiscountPolicy
    {
        public IEnumerable<Product> ApplyDiscount(IEnumerable<Product> product);
    }
}
