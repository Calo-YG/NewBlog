using Microsoft.AspNetCore.Authorization;

namespace Calo.Blog.Common.Authorization
{
    public class AuthorizeRequirement : IAuthorizationRequirement
    {
        public virtual string[] AuthorizeName { get; private set; }
        public AuthorizeRequirement(params string[] authorizeName)
        {
            AuthorizeName = authorizeName;
        }

        public AuthorizeRequirement() { }
    }
}
