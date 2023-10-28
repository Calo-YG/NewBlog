using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.DatabaseConext;

namespace Y.SqlsugarRepository.Entensions
{
    public static class DbExtensions
    {
        /// <summary>
        /// 单例注入
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugarWithUnitOfWorkAsSignleton(this IServiceCollection services, Action<ConnectionConfig> action)
        {

            ConnectionConfig config = new ConnectionConfig();
            action.Invoke(config);
            ISqlSugarClient sugar = new SqlSugarScope(config);
            services.AddSingleton<ISqlSugarClient>(sugar);
            ISugarUnitOfWork<SugarContext> context = new SugarUnitOfWork<SugarContext>(sugar);
            services.AddSingleton<ISugarUnitOfWork<SugarContext>>(context);
            return services;
        }
        /// <summary>
        /// 作用域注入SqlsugarClient
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugarClientAsScope(this IServiceCollection services, Action<ConnectionConfig> action)
        {

            ConnectionConfig config = new ConnectionConfig();
            config.ConfigureExternalServices = TableAttributeConfig.AddContextColumsConfigure();
            action.Invoke(config);
            services.AddScoped<ISqlSugarClient>(p =>
            {
                var suagr = new SqlSugarClient(config);
                return suagr;
            });
            return services;
        }
        /// <summary>
        /// 注入工作单元
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWorkScoped(this IServiceCollection services)
        {
            services.AddScoped<ISugarUnitOfWork<SugarContext>>(p =>
            {
                var sugar = p.GetRequiredService<ISqlSugarClient>();
                return new SugarUnitOfWork<SugarContext>(sugar);
            });
            return services;
        }
        /// <summary>
        /// 单例注入工作单元
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWorkSingleton(this IServiceCollection services)
        {
            services.AddSingleton<ISugarUnitOfWork<SugarContext>>(p =>
            {
                var sugar = p.GetRequiredService<ISqlSugarClient>();
                return new SugarUnitOfWork<SugarContext>(sugar);
            });
            return services;
        }
        /// <summary>
        /// 单例注入SqlSugarscope
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlsugarSignleton(this IServiceCollection services, Action<ConnectionConfig> action)
        {
            ConnectionConfig config = new ConnectionConfig();
            action.Invoke(config);
            ISqlSugarClient sugar = new SqlSugarScope(config);
            services.AddSingleton<ISqlSugarClient>(sugar);
            return services;
        }
    }
}
