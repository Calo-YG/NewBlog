using Calo.Blog.Common.UserSession;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Calo.Blog.Common.Middlewares
{
    public class UseWithShowSwaggerUI
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public UseWithShowSwaggerUI(RequestDelegate next
            , ILoggerFactory loggerFactory) 
        { 
            _next = next;
            _logger = loggerFactory.CreateLogger<UseWithShowSwaggerUI>();
        }

        public async Task Invoke(HttpContext context)
        {
            var identity = context.User?.Identity;
            var isAuthenticated = identity?.IsAuthenticated ?? false;
            var claims = context.User?.Claims;
            var userId = claims?.FirstOrDefault(p => p.Type == "Id")?.Value;
            if (userId is null || !isAuthenticated)
            {
                _logger.LogWarning("登陆系统才能使用Swagger");
                return;
            }
            await _next(context);
        }
    }
}
