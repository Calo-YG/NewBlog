using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace Calo.Blog.Common.Authorization
{
    public class AuthorizeMiddleHandle : IAuthorizationMiddlewareResultHandler
    {
        public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            if (!authorizeResult.Succeeded)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            return Task.CompletedTask;
        }
    }
}
