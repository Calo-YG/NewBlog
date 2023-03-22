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
            if (services is null) throw new ArgumentException("ModuleLoad中Services为空");
            List<IYModuleDescritor> result = new();
            LoadModules(startModuleType, result, services);
            //反向排序 保证被依赖的模块优先级高于依赖的模块
            result.Reverse();
            return result;
        }

        protected virtual void LoadModules(Type type, List<IYModuleDescritor> descritors, IServiceCollection services)
        {
            foreach (var item in ModuleHelper.LoadModules(type))
            {
                descritors.Add(CreateModuleDescritor(item, services));
            }
        }

        protected virtual IYModule CreateAndRegistModule(Type type, IServiceCollection services)
        {
            var module = Activator.CreateInstance(type) as IYModule;
            services.AddSingleton(type, module);
            return module;
        }



        private IYModuleDescritor CreateModuleDescritor(Type type, IServiceCollection services)
        {
            return new YModuleDescritor(type, CreateAndRegistModule(type, services));
        }


    }
}
