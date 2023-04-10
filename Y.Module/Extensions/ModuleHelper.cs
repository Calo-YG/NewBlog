using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Y.Module.DependencyInjection;
using Y.Module.Modules;

namespace Y.Module.Extensions
{
    public static class ModuleHelper
    {
        public static List<Type> LoadModules(Type modules)
        {
            YModule.CheckModuleType(modules);
            List<Type> types = new();
            AddModuleFrompepend(modules, types);
            return types;
        }
        /// <summary>
        /// 递归获取所有模块
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        private static List<Type> GetModuleDepend(Type moduleType, List<Type> types)
        {
            YModule.CheckModuleType(moduleType);

            var depedns = moduleType.GetCustomAttributes().OfType<DependOnAttribute>();

            foreach (var depend in depedns)
            {
                foreach (var itemType in depend.Types)
                {
                    AddModuleFrompepend(itemType, types);
                }
            }
            return types;
        }

        private static void AddModuleFrompepend(Type moduleType, List<Type> types)
        {
            YModule.CheckModuleType(moduleType);
            if (types.Contains(moduleType))
            {
                return;
            }
            types.Add(moduleType);
            foreach (var type in GetModuleDepend(moduleType, types))
            {
                AddModuleFrompepend(moduleType, types);
            }
        }

        public static void AddAssembly(this IServiceCollection services, Assembly assembly)
        {
            services.ChcekNull();
            new InjectionFactory().InjectionAssembly(services, assembly);
        }
    }
}
