using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.DependencyInjection
{
    public class InjectionFactory
    {
        public void InjectionAssembly(Assembly assembly)
        {
            var injectionTypes = new List<Type>()
            {
                typeof(ISingletonInjection)
                ,typeof(IScopedInjection)
                ,typeof(ITransientInjection)
            };
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var firstInterface = type.GetInterfaces().FirstOrDefault();
                if (firstInterface != null && !injectionTypes.Contains(firstInterface))
                {

                }
            }
        }
        protected virtual Type? GetInterfaceTypeByInjectionAttribute(Type type)
        {
            var attributes = type.GetCustomAttributes().Cast<InjectionAttribute>();
            var injectionAttribute = attributes.FirstOrDefault();
            if (!attributes.Any() || injectionAttribute is null || injectionAttribute.InterfaceType is null)
            {
                return null;
            }
            return injectionAttribute.InterfaceType;
        }
    }
}
