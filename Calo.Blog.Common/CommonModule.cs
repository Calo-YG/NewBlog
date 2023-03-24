using Calo.Blog.Common.Authorization;
using Calo.Blog.Common.CustomOptions;
using Calo.Blog.Common.Filters;
using Calo.Blog.Common.Redis;
using Calo.Blog.Extenions.AjaxResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using Y.Module;
using Y.Module.Modules;
using Y.Module.Extensions;
using Microsoft.AspNetCore.Builder;
using Calo.Blog.Common.Middlewares;
using Calo.Blog.Common.UserSession;

namespace Calo.Blog.Common
{
    public class CommonModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
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

            //使用CsRedis
            var csredis = new CSRedis.CSRedisClient("124.71.15.19:6379,password=154511,defaultDatabase=1,ssl=false,writeBuffer=10240,poolsize=50,prefix=Y");
            context.Services.AddSingleton<IDistributedCache>(new CSRedisCache(csredis));
            RedisHelper.Initialization(csredis);

            context.Services.AddSingleton<ICacheManager, CacheManager>();

            context.Services.AddControllers(options =>
            {
                options.Filters.Add<ResultFilter>();
            });

            Configure<ExceptionOptions>(p =>
            {
                p.UseDataBase = false;
            });
        }

        public override void InitApplication(InitApplicationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
