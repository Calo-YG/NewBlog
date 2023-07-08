using Calo.Blog.Common;
using Calo.Blog.Common.Y.EventBus;
using Calo.Blog.EntityCore;
using Calo.Blog.EntityCore.DataBase.Entities;
using Microsoft.Extensions.DependencyInjection;
using Y.Module;
using Y.Module.Modules;

namespace Calo.Blog.Domain
{
    [DependOn(typeof(CommonModule))]
    public class BlogCoreModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            context.Services.AddSingleton<IEventBus, EventBus>();
            context.Services.AddEventHandle(p =>
            {
                p.AddEventHandle<User, TestHandle>();
            });
        }

        public override void LaterInitApplication(InitApplicationContext context)
        {
            var provider = context.ServiceProvider;

        }
    }
}
