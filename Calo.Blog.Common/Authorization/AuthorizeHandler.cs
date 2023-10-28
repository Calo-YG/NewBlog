﻿using Microsoft.AspNetCore.Authentication;
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
            AuthorizationFailureReason failureReason;
            //判断是否通过鉴权中间件--是否登录
            if (userId is null || !isAuthenticated)
            {
                failureReason = new AuthorizationFailureReason(this, "请登录到系统");
                context.Fail(failureReason);
                return;
            }

            var defaultPolicy = requirement.AuthorizeName?.Any() ?? false;
            //默认授权策略
            if (!defaultPolicy)
            {
                context.Succeed(requirement);
                return;
            }
            UserTokenModel tokenModel = new UserTokenModel()
            {
                UserId = userId ?? "",
                UserName = claims?.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value ?? "",
            };
            if (requirement?.AuthorizeName?.Any() ?? false)
            {
                if (!_permisscheck.IsGranted(tokenModel, requirement.AuthorizeName))
                {
                    failureReason = new AuthorizationFailureReason(this, $"权限不足，无法请求--请求接口{httpContext?.Request?.Path ?? ""}");
                    context.Fail(failureReason);
                    return;
                }
            }
            context.Succeed(requirement);
        }

    }
}
