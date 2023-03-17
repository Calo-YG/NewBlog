using Microsoft.AspNetCore.Authorization;

namespace Calo.Blog.Common.Authorization
{
    public class AuthorizationProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            throw new NotImplementedException();
        }
    }
}
