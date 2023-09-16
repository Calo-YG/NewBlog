using Calo.Blog.Common;
using Y.Module;
using Y.Module.Modules;

namespace Calo.Blog.Domain
{
    [DependOn(typeof(CommonModule))]
    public class BlogCoreModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
        }

        public override void LaterInitApplication(InitApplicationContext context)
        {
            var provider = context.ServiceProvider;

        }
    }
}
