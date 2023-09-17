﻿using Calo.Blog.Application.ResourceOwnereServices.Etos;
using Calo.Blog.Common.Authorization.Authorize;
using Calo.Blog.Domain;
using Calo.Blog.EntityCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Y.EventBus;
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

            context.Services.AddEventBus();

            context.Services.AddChannles(p =>
            {
                p.TryAddChannle<TestEto>();
            });
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

            await authorizeManager.AddAuthorizeRegiester();

            await eventhandlerManager.CreateChannles();

            eventhandlerManager.Subscribe<TestEto>();
        }
    }
}
