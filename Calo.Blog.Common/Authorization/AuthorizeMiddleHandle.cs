using Calo.Blog.Extenions.AjaxResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Calo.Blog.Common.Authorization
{
    public class AuthorizeMiddleHandle : IAuthorizationMiddlewareResultHandler
    {
        private readonly ILogger _logger;
        public AuthorizeMiddleHandle(ILoggerFactory loggerFactory) 
        {
            _logger = loggerFactory.CreateLogger<AuthorizeMiddleHandle>();  
        }
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (!authorizeResult.Succeeded || authorizeResult.Challenged)
            {
                var reason = authorizeResult?.AuthorizationFailure?.FailureReasons.FirstOrDefault();
                var isLogin = context?.User?.Identity?.IsAuthenticated ?? false;
                var path = context?.Request?.Path ?? "";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var response = new AjaxResponse();
                response.UnAuthorizedRequest = true;
                response.StatusCode = "401";
                var error = new ErrorInfo();
                error.Error = reason?.Message ?? "Token异常或者过期";
                response.Error = error;
                await context.Response.WriteAsJsonAsync(response);
                _logger.LogWarning(error.Error);
                return;
            }
            await next(context);
        }
    }
}
