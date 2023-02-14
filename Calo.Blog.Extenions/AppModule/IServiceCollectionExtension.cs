using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    internal static class IServiceCollectionExtension
    {
        /// <summary>
        /// 获取单例注入对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        private static T? GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(T) && d.Lifetime == ServiceLifetime.Singleton);
            return descriptor?.ImplementationInstance is not null
                ? (T)descriptor.ImplementationInstance
                : descriptor?.ImplementationFactory is not null ? (T)descriptor.ImplementationFactory.Invoke(null!) : default;
        }
        public static T GetService<T>(this IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<T>();
        }
        private static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            return service is null ? throw new InvalidOperationException($"找不到Singleton服务: {typeof(T).AssemblyQualifiedName}") : service;
        }
        public static ObjectAccessor<T> TryAddIObjectAccessor<T>(this IServiceCollection services){
          return services.Any(p=>p.ServiceType==typeof(ObjectAccessor<T>)) ?services.GetSingletonInstance<ObjectAccessor<T>>() : services.GetService<ObjectAccessor<T>>();
        }

    }
}
