using Calo.Blog.Extenions.AjaxResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Calo.Blog.Common.Authorization
{
    public class AuthorizeMiddleHandle : IAuthorizationMiddlewareResultHandler
    {
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (!authorizeResult.Succeeded || authorizeResult.Challenged)
            {
                var path = context.Request.Path;
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var response = new AjaxResponse();
                response.UnAuthorizedRequest = true;
                response.StatusCode = "401";
                var error = new ErrorInfo();
                error.Error = "你没有权限访问该接口";
                response.Error = error;
                await context.Response.WriteAsJsonAsync(response);
                return;
            }
            await next(context);
        }
    }
}
