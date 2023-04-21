using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata.Ecma335;
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

        public IReadOnlyList<IYModuleDescritor> Modules { get; private set; }

        private bool isConfigService;

        public ModuleApplicationBase(Type startModuleType, IServiceCollection services)
        {
            YModule.CheckModuleType(startModuleType);
            services.ChcekNull();
            StartModuleType = startModuleType;
            Services = services;
            isConfigService = false;

            services.TryAddIObjectAccessor<IServiceProvider>();
            services.TryAddObjectAccessor<IServiceProvider>();

            Services.TryAddIObjectAccessor<InitApplicationContext>();
            Services.TryAddObjectAccessor<InitApplicationContext>();

            Services.AddSingleton<IModuleContainer>(this);
            Services.AddSingleton<IModuleApplication>(this);



            Modules = LoadModules();

            ConfigerService();
        }

        public virtual void ConfigerService()
        {
            if (isConfigService) return;

            ConfigerServiceContext context = new ConfigerServiceContext(Services);

            foreach (var module in Modules)
            {
                if (module.Incetance is YModule Module)
                {
                    Module.ConfigerServiceContext = context;
                }
            }

            //PreInitApplication
            try
            {
                foreach (var module in Modules)
                {
                    //初始化之前处理
                    if (module.Incetance is IPreConfigServices application)
                    {
                        application.PreInitApplication(context);
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }

            //ConfigerService
            try
            {

                foreach (var module in Modules)
                {
                    module.Incetance.ConfigerService(context);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            isConfigService = true;
        }

        protected virtual void SetServiceProvider(IServiceProvider servicrovider)
        {
            ServiceProvider = servicrovider;
            Services.GetSingleton<ObjectAccessor<IServiceProvider>>().Value = servicrovider;
        }

        public virtual void InitApplication(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            scope.ServiceProvider
                .GetRequiredService<IModuleManager>()
                .IninAppliaction();
        }


        protected virtual IReadOnlyList<IYModuleDescritor> LoadModules()
        {
            return new ModuleLoad().GetYModuleDescritors(StartModuleType, Services);
        }

    }
}
