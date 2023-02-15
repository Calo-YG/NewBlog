using Microsoft.Extensions.DependencyInjection;
using Y.Module.Extensions;
using Y.Module.Interfaces;
using Y.Module.Modules;

namespace Y.Module
{
    public class ModuleLoad : IModuleLoad
    {

        public List<IYModuleDescritor> GetYModuleDescritors(Type startModuleType, IServiceCollection services)
        {
            List<IYModuleDescritor> result = new();
            LoadModules(startModuleType, result, services);
            YModuleDescritor yModuleDescritor = new YModuleDescritor();
            yModuleDescritor.SetMoudleDescritor(result);
            services.AddSingleton<IYModuleDescritor>(yModuleDescritor);
            return result;
        }

        private void LoadModules(Type type, List<IYModuleDescritor> descritors, IServiceCollection services)
        {
            foreach (var item in ModuleHelper.LoadModules(type))
            {
                descritors.Add(CreateModuleDescritor(item, CreateAndRegistModule(item, services)));
            }
        }

        private IYModule? CreateAndRegistModule(Type type, IServiceCollection services)
        {
            if (services is null) throw new ArgumentException("ModuleLoad中Services为空");
            var module = Activator.CreateInstance(type) as IYModule;
            if (module is null) throw new ApplicationException("反射获取的Module为空");
            services.AddSingleton(type, module);
            return module;
        }

        private IYModuleDescritor CreateModuleDescritor(Type type, IYModule module)
        {
            return new YModuleDescritor(type, module);
        }


    }
}
