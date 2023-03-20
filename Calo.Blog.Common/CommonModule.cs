using Calo.Blog.Common.Authorization;
using Calo.Blog.Common.Filters;
using Calo.Blog.Common.Redis;
using Calo.Blog.Extenions.AjaxResponse;
using FreeRedis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Y.Module;
using Y.Module.Modules;

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

            //使用Free Redis
            context.Services.AddSingleton<IRedisClient, RedisClient>(p => new RedisClient("124.71.15.19:6379,password=154511,defaultDatabase=1"));
            context.Services.AddSingleton<ICacheManager, CacheManager>();

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
