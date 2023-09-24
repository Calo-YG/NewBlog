using Y.Module;
using Y.Module.Extensions;
using Y.Module.Modules;

namespace Calo.Blog.Common.Redis
{
    public class RedisModule:YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            var configuration = context.GetConfiguartion();

            context.Services.AddRedis(configuration);
        }
    }
}
