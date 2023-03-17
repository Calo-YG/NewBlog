using Calo.Blog.Common.Authorization;
using Calo.Blog.Common.Filters;
using Calo.Blog.Extenions.AjaxResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Y.Module;
using Y.Module.Modules;

namespace Calo.Blog.Common
{
    public class CommonModule:YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            //统一返回值处理工厂
            context.Services.AddScoped<IActionResultWrapFactory, FilterResultWrapFactory>();
            context.Services.AddSingleton<ITokenProvider,TokenProvider>();
            //权限检测程序
            context.Services.AddScoped<IPermissionCheck, PermissionCheck>();
            context.Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationProvider>();
            context.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizeMiddleHandle>();

            context.Services.AddControllers(options =>
            {
                options.Filters.Add<ResultFilter>();
            });
        }

        public override void InitApplication(InitApplicationContext context)
        {
            
        }
    }
}
