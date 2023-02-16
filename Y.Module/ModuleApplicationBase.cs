using Microsoft.Extensions.DependencyInjection;
using Y.Module.Extensions;
using Y.Module.Interfaces;
using Y.Module.Modules;

namespace Y.Module
{
    public class ModuleApplicationBase : IModuleApplication
    {
        public Type StartModuleType { get; private set; }

        public IServiceCollection Services { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        public ModuleApplicationBase(Type startModuleType, IServiceCollection services)
        {
            YModule.CheckModuleType(startModuleType);
            services.ChcekNull();
            StartModuleType = startModuleType;
            Services = services;
        }

        public virtual void ConfigerService()
        {
            throw new NotImplementedException();
        }

        public virtual void InitApplication()
        {
            throw new NotImplementedException();
        }
    }
}
