namespace DependencyInjection.Sample.TightlyCoupled.Core
{
    public static class UserContext
    {
        public static User GetCurrentUser() => new User() { Name = Environment.UserName, IsPreferredCustomer = true };

        public class User
        {
            public string Name { get; set; }
            public bool IsPreferredCustomer { get; set; }
        }
    }
}
