using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Calo.Blog.Common.Authorization
{
    public class AuthorizeHandler : AuthorizationHandler<AuthorizeRequirement>
    {
        private readonly IPermissionCheck _permisscheck;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationSchemeProvider _authenticationSchemes;

        public AuthorizeHandler(IHttpContextAccessor httpContextAccessor
            , IServiceProvider serviceProvider
            , IAuthenticationSchemeProvider authenticationSchemes)
        {
            using var scope = serviceProvider.CreateScope();
            _permisscheck = scope.ServiceProvider.GetRequiredService<IPermissionCheck>();
            _httpContextAccessor = httpContextAccessor;
            _authenticationSchemes = authenticationSchemes;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeRequirement requirement)
        {
            var identity = _httpContextAccessor?.HttpContext?.User?.Identity;
            var httpContext = _httpContextAccessor?.HttpContext;
            var isAuthenticated = identity?.IsAuthenticated ?? false;
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var userId = claims?.FirstOrDefault(p => p.Type == "Id")?.Value;
            var schemes = await _authenticationSchemes.GetAllSchemesAsync();
            var handlers = httpContext?.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            foreach (var scheme in schemes)
            {
                //判断请求是否停止
                if (handlers?.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler requestHandler && await requestHandler.HandleRequestAsync())
                {
                    context.Fail();
                    return;
                }
            }
            //判断是否通过鉴权中间件--是否登录
            if (userId is null || !isAuthenticated)
            {
                context.Fail();
                return;
            }
            //默认授权策略
            if (requirement.AuthorizeName is null || !requirement.AuthorizeName.Any())
            {
                context.Succeed(requirement);
                return;
            }
            var roleIds = claims?
                .Where(p => p?.Type?.Equals("RoleIds") ?? false)
                .Select(p => long.Parse(p.Value));
            var roleNames = claims?
                .Where(p => p?.Type?.Equals(ClaimTypes.Role) ?? false)
                .Select(p => p.Value);
            UserTokenModel tokenModel = new UserTokenModel()
            {
                UserId = long.Parse(userId ?? "0"),
                UserName = claims?.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value ?? "",
                RoleNames = roleNames?.ToArray(),
                RoleIds = roleIds?.ToArray(),
            };
            if (requirement.AuthorizeName.Any())
            {
                if (!_permisscheck.IsGranted(tokenModel, requirement.AuthorizeName))
                {
                    context.Fail();
                    return;
                }
            }
            context.Succeed(requirement);
        }

    }
}
