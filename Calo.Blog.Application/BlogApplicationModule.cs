using Calo.Blog.Application.RegiesterPermissions;
using Calo.Blog.Application.ResourceOwnereServices.Etos;
using Calo.Blog.Application.ResourceOwnereServices.Handlers;
using Calo.Blog.Common.Authorization.Authorize;
using Calo.Blog.Common.Minio;
using Calo.Blog.Common.Redis;
using Calo.Blog.Domain;
using Calo.Blog.EntityCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Y.EventBus;
using Y.Module;
using Y.Module.Extensions;
using Y.Module.Modules;
using Yitter.IdGenerator;

namespace Calo.Blog.Application
{
    [DependOn(typeof(BlogCoreModule)
        , typeof(SqlSugarEnityCoreModule)
        ,typeof(RedisModule)
        ,typeof(MinioModule))]
    public class BlogApplicationModule : YModule
    {
        /// <summary>
        /// 预初始化
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigerService(ConfigerServiceContext context)
        {
            AuthorizeRegister.Register.RegisterAuthorizeProvider<TestAuthorizePermissionProvider>();
            var options = new IdGeneratorOptions()
            {
                SeqBitLength=10
            };
            YitIdHelper.SetIdGenerator(options);
        }
        /// <summary>
        /// 服务注入
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigerService(ConfigerServiceContext context)
        {
            context.Services.AddAssembly(Assembly.GetExecutingAssembly());

            context.Services.AddEventBus();

            context.Services.Subscribes(p =>
            {
                p.Subscribe<TestEto, TestEventHandler>();
            });
            AuthorizeRegister.Register.Init(context.Services);
        }
        /// <summary>
        /// 同步初始化
        /// </summary>
        /// <param name="context"></param>
        public override async Task LaterInitApplicationAsync(InitApplicationContext context)
        {
            var scope = context.ServiceProvider.CreateScope();

            var authorizeManager = scope.ServiceProvider.GetRequiredService<IAuthorizeManager>();

            var eventhandlerManager = scope.ServiceProvider.GetRequiredService<IEventHandlerManager>();

            IAuthorizePermissionContext permissionContext = new AuthorizePermissionContext();
            await authorizeManager.AddAuthorizeRegiester(permissionContext);
        }
    }
}
