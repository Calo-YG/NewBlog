using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static IServiceCollection AddSqlSugarWithUnitOfWorkAsSignleton<TUnitofWork>(this IServiceCollection services, Action<ConnectionConfig> action)
            where TUnitofWork : SugarUnitOfWork, new()
        {

            ConnectionConfig config = new ConnectionConfig();
            action.Invoke(config);
            ISqlSugarClient sugar = new SqlSugarScope(config);
            services.AddSingleton<ISqlSugarClient>(sugar);
            ISugarUnitOfWork<TUnitofWork> context = new SugarUnitOfWork<TUnitofWork>(sugar);
            services.AddSingleton<ISugarUnitOfWork<TUnitofWork>>(context);
            return services;
        }
        /// <summary>
        /// 作用域注入SqlsugarClient
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugarClientAsCleint(this IServiceCollection services, Action<ConnectionConfig> action)
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
        public static IServiceCollection AddUnitOfWorkAsScoped<TUnitOfWork>(this IServiceCollection services) where TUnitOfWork : SugarUnitOfWork, new()
        {
            services.AddScoped<ISugarUnitOfWork<TUnitOfWork>>(p =>
            {
                var sugar = p.GetRequiredService<ISqlSugarClient>();
                return new SugarUnitOfWork<TUnitOfWork>(sugar);
            });
            return services;
        }
        /// <summary>
        /// 注入工作单元
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWorkAsTransint<TUnitOfWork>(this IServiceCollection services) where TUnitOfWork : SugarUnitOfWork, new()
        {
            services.AddTransient<ISugarUnitOfWork<TUnitOfWork>>(p =>
            {
                var sugar = p.GetRequiredService<ISqlSugarClient>();
                return new SugarUnitOfWork<TUnitOfWork>(sugar);
            });
            return services;
        }
        /// <summary>
        /// 注入工作单元
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSuagarDbContextAsSignledton<TUnitOfWork>(this IServiceCollection services) where TUnitOfWork : SugarUnitOfWork, new()
        {
            services.AddSingleton<ISugarUnitOfWork<TUnitOfWork>>(p =>
            {
                var sugar = p.GetRequiredService<ISqlSugarClient>();
                return new SugarUnitOfWork<TUnitOfWork>(sugar);
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
