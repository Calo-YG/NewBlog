using Calo.Blog.Common.Authorization.Authorize;
using Calo.Blog.Domain;
using Calo.Blog.EntityCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Y.Module;
using Y.Module.Extensions;
using Y.Module.Modules;

namespace Calo.Blog.Application
{
    [DependOn(typeof(BlogCoreModule)
        , typeof(SqlSugarEnityCoreModule))]
    public class BlogApplicationModule : YModule
    {
        /// <summary>
        /// 预初始化
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigerService(ConfigerServiceContext context)
        {
            base.PreConfigerService(context);
        }
        /// <summary>
        /// 服务注入
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigerService(ConfigerServiceContext context)
        {
            context.Services.AddAssembly(Assembly.GetExecutingAssembly());
        }
        /// <summary>
        /// 同步初始化
        /// </summary>
        /// <param name="context"></param>
        public override void LaterInitApplication(InitApplicationContext context)
        {
            var scope = context.ServiceProvider.CreateScope();
            var authorizeManager = scope.ServiceProvider.GetRequiredService<IAuthorizeManager>();
            authorizeManager.AddAuthorizeRegiester();
        }
    }
}
