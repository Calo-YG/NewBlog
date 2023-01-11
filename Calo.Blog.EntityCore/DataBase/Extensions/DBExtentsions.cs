using Autofac.Core;
using Calo.Blog.EntityCore.DataBase.DatabaseContext;
using Calo.Blog.Extenions.EnumExtenions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Org.BouncyCastle.Crypto.Tls;
using SqlSugar;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Extensions
{
    public static class DBExtentsions
    {
        /// <summary>
        /// 单例注入
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugarDbContextAsSignleton<TDbContext>(this IServiceCollection services, Action<ConnectionConfig> action)
            where TDbContext : SugarUnitOfWork, new()
        {

            ConnectionConfig config = new ConnectionConfig();
            config.ConfigureExternalServices = TableAttributeConfig.AddContextColumsConfiure();
            action.Invoke(config);
            ISqlSugarClient sugar = new SqlSugarScope(config);
            services.AddSingleton<ISqlSugarClient>(sugar);
            sugar.QueryFilter.ConfigureFilterForEntity();
            ISugarUnitOfWork<TDbContext> context = new SugarUnitOfWork<TDbContext>(sugar);
            services.AddSingleton<ISugarUnitOfWork<TDbContext>>(context);
            return services;
        }
        /// <summary>
        /// 作用域注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlSugarClientAsCleint(this IServiceCollection services, Action<ConnectionConfig> action)
        {

            ConnectionConfig config = new ConnectionConfig();
            config.ConfigureExternalServices = TableAttributeConfig.AddContextColumsConfiure();
            action.Invoke(config);
            services.AddScoped<ISqlSugarClient>(p =>
            {
                var suagr = new SqlSugarClient(config);
                suagr.QueryFilter.ConfigureFilterForEntity();
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
        public static IServiceCollection AddSuagarDbContextAsScoped<TDbContext>(this IServiceCollection services) where TDbContext : SugarUnitOfWork, new()
        {
            services.AddScoped<ISugarUnitOfWork<TDbContext>>(p =>
            {
                var sugar = p.GetRequiredService<ISqlSugarClient>();
                return new SugarUnitOfWork<TDbContext>(sugar);
            });
            return services;
        }
        /// <summary>
        /// 注入工作单元
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSuagarDbContextAsTransint<TDbContext>(this IServiceCollection services) where TDbContext : SugarUnitOfWork, new()
        {
            services.AddTransient<ISugarUnitOfWork<TDbContext>>(p =>
            {
                var sugar = p.GetRequiredService<ISqlSugarClient>();
                return new SugarUnitOfWork<TDbContext>(sugar);
            });
            return services;
        }
        /// <summary>
        /// 注入工作单元
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSuagarDbContextAsSignledton<TDbContext>(this IServiceCollection services) where TDbContext : SugarUnitOfWork, new()
        {
            services.AddSingleton<ISugarUnitOfWork<TDbContext>>(p =>
            {
                var sugar = p.GetRequiredService<ISqlSugarClient>();
                return new SugarUnitOfWork<TDbContext>(sugar);
            });
            return services;
        }
    }
}
