using Calo.Blog.Common.Authorization;
using Calo.Blog.Common.CustomOptions;
using Calo.Blog.Common.Filters;
using Calo.Blog.Extenions.AjaxResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Y.Module;
using Y.Module.Modules;
using Y.Module.Extensions;
using Calo.Blog.Common.UserSession;
using System.Reflection;

namespace Calo.Blog.Common
{
    public class CommonModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            var configuration = context.GetConfiguartion();

            //统一返回值处理工厂
            context.Services.AddScoped<IActionResultWrapFactory, FilterResultWrapFactory>();
            context.Services.AddSingleton<ITokenProvider, TokenProvider>();

            //权限检测程序
            context.Services.AddScoped<IPermissionCheck, PermissionCheck>();
            context.Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationProvider>();
            context.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizeMiddleHandle>();
            context.Services.AddSingleton<IAuthorizationHandler, AuthorizeHandler>();
            //注入IUserSession
            context.Services.AddScoped<IUserSession, CurrentUserSession>();
            //添加权限
            //context.Services.AddSingleton<IAuthorizeManager, AuthorizeManager>();

            context.Services.AddControllers(options =>
            {
                options.Filters.Add<ResultFilter>();
            });

            Configure<ExceptionOptions>(p =>
            {
                p.UseDataBase = false;
            });

            ///程序集注入
            context.Services.AddAssembly(Assembly.GetExecutingAssembly());
        }

        public override  Task LaterInitApplicationAsync(InitApplicationContext context)
        {
           return  base.LaterInitApplicationAsync(context);
        }
    }
}
