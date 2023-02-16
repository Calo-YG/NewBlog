using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Interfaces;

namespace Y.Module.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void TryAddObjectAccessor<T>(this IServiceCollection services, T obj)
        {
            if (services.Any(p => p.ServiceType == typeof(T))) return;
            var objectAccessor = new ObjectAccessor<T>(obj);
            services.Insert(0, ServiceDescriptor.Singleton<IObjectAccessor<T>>(objectAccessor));
        }

        public static bool IsExists(this IServiceCollection services, Type type)
        {
            return services.Any(p => p.ServiceType == type);
        }

        public static bool IsExists<T>(this IServiceCollection services)
        {
            return services.IsExists(typeof(T));
        }

        public static T? GetSingletonOrNull<T>(this IServiceCollection services)
        {
            return (T?)services.FirstOrDefault(p => p.ServiceType == typeof(T))?.ImplementationInstance;
        }

        public static T GetSingleton<T>(this IServiceCollection services)
        {
            var obj = GetSingletonOrNull<T>(services);
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            return obj;
        }

        public static void ChcekNull(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException("IServiceCollection为空");
            }
        }
    }
}
