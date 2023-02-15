using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Modules;

namespace Y.Module.Extensions
{
    public static class ModuleHelper
    {
        public static List<Type> LoadModules(Type modules)
        {
            List<Type> types = new();
            return GetAllModules(modules, types);
        }
        /// <summary>
        /// 递归获取所有模块
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        private static List<Type> GetAllModules(Type moduleType, List<Type> types)
        {
            if (moduleType.BaseType == typeof(YModule) && types.FirstOrDefault(p => p.Name == moduleType.Name) is null)
            {
                types.Add(moduleType);
            }
            var attributes = moduleType.GetCustomAttributes(false)
                .FirstOrDefault(p => p.GetType() == typeof(DependenOnAttribute));
            if (attributes is not null && attributes is DependenOnAttribute)
            {
                var modules = (attributes as DependenOnAttribute).Types;
                if (modules is not null)
                {
                    foreach (var module in modules)
                    {
                        types.AddRange(GetAllModules(module, types));
                    }
                }
            }
            return types;
        }
    }
}
