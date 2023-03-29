using DependencyInjection.Sample.LooselyCoupled.Core;

namespace DependencyInjection.Sample.LooselyCoupled.Application
{
    internal class LoggedInUserAccessor : IUserContextAccessor
    {
        public User GetCurrentUser()
        {
            return new User()
            {
                Name = Environment.UserName,
                IsPreferredCustomer = true
            };
        }
    }
}
