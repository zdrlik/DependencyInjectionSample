using DependencyInjection.Sample.LooselyCoupled.Core;

namespace DependencyInjection.Sample.LooselyCoupled.WebApi
{
    public class HttpContextUserAccessor : IUserContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public User GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
            return new User()
            {
                Name = user.Identity.Name,
                IsPreferredCustomer = user.IsInRole("PreferredCustomer")
            };
        }
    }
}
