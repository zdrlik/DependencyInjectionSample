namespace DependencyInjection.Sample.LooselyCoupled.Core.Discounts
{
    public interface IDiscountPolicyProvider
    {
        IDiscountPolicy GetDiscountPolicy(User forUser);
    }
}
