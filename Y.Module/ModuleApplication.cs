using Microsoft.Extensions.DependencyInjection;
using Y.Module.Interfaces;

namespace Y.Module
{
    public class ModuleApplication : ModuleApplicationBase, IModuleRunner
    {
        public ModuleApplication(Type startModuleType, IServiceCollection services) : base(startModuleType, services)
        {
            services.AddSingleton<IModuleRunner>(this);
        }

        public override void InitApplication(IServiceProvider serviceProvider)
        {
            SetServiceProvider(serviceProvider);
            base.InitApplication(serviceProvider);
        }

        public override async Task InitApplicationAsync(IServiceProvider serviceProvider)
        {
            SetServiceProvider(serviceProvider);

            await base.InitApplicationAsync(serviceProvider);
        }
    }
}
