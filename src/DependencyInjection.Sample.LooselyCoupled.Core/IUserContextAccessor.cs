namespace DependencyInjection.Sample.LooselyCoupled.Core
{
    public interface IUserContextAccessor
    {
        public User GetCurrentUser();
    }

    public class User
    {
        public string Name { get; set; }
        public bool IsPreferredCustomer { get; set; }
    }
}
