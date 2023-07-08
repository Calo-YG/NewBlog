using Microsoft.Extensions.DependencyInjection;
using Y.Module;
using Y.Module.Modules;

namespace FreeInterface
{
    public class FreeInterfaceModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            context.Services.AddHttpApi<ICommonHttpApi>();
        }
    }
}
