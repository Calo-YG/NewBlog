using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{

    public abstract class YModule : IYModule, IPreApplicationInition
    {
        public virtual void ServiceConfiguration(IServiceCollection services)
        {

        }
        public virtual void ApplictionInit(IServiceProvider serviceProvider)
        {

        }
        public virtual void PreApplictionInition(IServiceProvider serviceProvider)
        {

        }

        public static bool IsModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IYModule).GetTypeInfo().IsAssignableFrom(type);
        }
        public static void FindModuleDependon(Type module)
        {
            if (!IsModule(module))
            {
                throw new ApplicationException($"{module.Name} base type not YModule");
            }

            List<Type> types = new();

            module.GetCustomAttributes();
        }
    }
}
