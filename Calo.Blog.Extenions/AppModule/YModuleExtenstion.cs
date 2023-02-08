using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
