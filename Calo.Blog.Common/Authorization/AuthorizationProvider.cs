using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Calo.Blog.Common.Authorization
{
    public class AuthorizationProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddAuthenticationSchemes("Bearer");
            policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
            policy.AddRequirements(new AuthorizeRequirement());
            return Task.FromResult<AuthorizationPolicy>(policy.Build());
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddAuthenticationSchemes("Bearer");
            policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
            if (policyName is null)
            {
                return Task.FromResult<AuthorizationPolicy>(null);
            }
            var authorizations = policyName.Split(',');
            if (authorizations.Any())
            {
                policy.AddRequirements(new AuthorizeRequirement(authorizations));
            }
            return Task.FromResult(policy.Build());
        }
    }
}
