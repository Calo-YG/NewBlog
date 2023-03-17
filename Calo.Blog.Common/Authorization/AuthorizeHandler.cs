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

        public AuthorizeHandler(IHttpContextAccessor httpContextAccessor,IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            _permisscheck = scope.ServiceProvider.GetRequiredService<IPermissionCheck>();
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizeRequirement requirement)
        {
            var identity = _httpContextAccessor?.HttpContext?.User?.Identity;
            var isAuthenticated = identity?.IsAuthenticated ?? false;
            //判断是否通过鉴权中间件
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var userId = claims?.FirstOrDefault(p => p.Type == "Id")?.Value;
            if (userId is null || !isAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            var roleIds = claims?
                .Where(p => p?.Type?.Equals("RoleIds") ?? false)
                .Select(p => long.Parse(p.Value));
            var roleNames = claims?
                .Where(p => p?.Type?.Equals(ClaimTypes.Role) ?? false)
                .Select(p => p.Value);
            UserTokenModel tokenModel = new UserTokenModel()
            {
                UserId= long.Parse(userId??"0"),
                UserName= claims?.FirstOrDefault(p=>p.Type== ClaimTypes.Name)?.Value ?? "",
                RoleNames = roleNames?.ToArray(),
                RoleIds= roleIds?.ToArray(),
            };
            if (requirement.AuthorizeName.Any())
            {
                if (_permisscheck.IsGranted(tokenModel, requirement.AuthorizeName))
                {
                    context.Succeed(requirement);
                }

            }
            return Task.CompletedTask;
        }

    }
}
