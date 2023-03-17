using Microsoft.AspNetCore.Authorization;

namespace Calo.Blog.Common.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        public virtual string[] AuthorizeName { get; private set; }

        public CustomAuthorizationAttribute(params string[] authorizeName)
        {
            AuthorizeName = authorizeName;
        }
    }
}
