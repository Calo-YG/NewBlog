using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Authorization
{
    public class AuthorizeHandler : AuthorizationHandler<AuthorizeRequirement>
    {
        private readonly IPermissionCheck _permisscheck;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizeHandler(IPermissionCheck permisscheck, IHttpContextAccessor httpContextAccessor)
        {
            _permisscheck = permisscheck;
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
            }
            var roleIds = claims?
                .Where(p => p is not null || p.Type.Equals("RoleIds"))
                .Select(p => long.Parse(p.Value));
            var roleNames = claims?
                .Where(p => p is not null || p.Type.Equals("Role"))
                .Select(p => p.Value);
            if (requirement.AuthorizeName.Any())
            {


            }
            return Task.CompletedTask;
        }

    }
}
