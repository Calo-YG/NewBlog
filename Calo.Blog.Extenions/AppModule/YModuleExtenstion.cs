using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    public static class YModuleExtenstion
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IConfiguration Configuration(this ServiceConfigurationContext context)
        {
            if (context is null || context.Service is null) throw new ArgumentException("context is null");
            return context.ServiceProvider.GetRequiredService<IConfiguration>();
        }

        public static IHostingEnvironment Environment(this ServiceConfigurationContext context)
        {
            if (context is null || context.Service is null) throw new ArgumentException("context is null");
            return context.ServiceProvider.GetRequiredService<IHostingEnvironment>();
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static void LoadModule<TModule>(this IServiceCollection services) where TModule : YModule
        {
            if (services is null) throw new ArgumentException("IServiceCollection Is Null In Module");
            List<Type> types = new List<Type>();
            types = GetAllModules(typeof(TModule), types);
            foreach (var module in types)
            {
                var mod = Activator.CreateInstance(module) as YModule;
                if (mod is null) throw new ArgumentException("模块加载异常");
                if (mod is YModule)
                {
                    (mod as YModule).ServiceConfiguration(new ServiceConfigurationContext(services));
                    using var provider = services.BuildServiceProvider();
                    (mod as YModule).PreApplictionInition(provider);
                }
            }
        }
        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="builder"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void InitModule<TModule>(this IApplicationBuilder builder)
        {
            if (builder is null) throw new ArgumentException("IApplicationBuilder Is Null In Module");
        }

        private static List<Type> GetAllModules(Type type, List<Type> types)
        {
            if (type.BaseType == typeof(YModule) && types.FirstOrDefault(p => p.Name == type.Name) is null)
            {
                types.Add(type);
            }
            var attributes = type.GetCustomAttributes(false)
                .FirstOrDefault(p => p.GetType() == typeof(DependOnAttribute));
            if (attributes is not null && attributes is DependOnAttribute)
            {
                var modules = (attributes as DependOnAttribute).Type;
                foreach (var module in modules)
                {
                    types.AddRange(GetAllModules(module, types));
                }
            }
            return types;
        }
    }
}
