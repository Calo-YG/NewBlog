using Calo.Blog.Extenions.EnumExtenions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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

        public static ConcurrentDictionary<LifeTime, Action<IServiceCollection>> dic;
        public static IServiceCollection AddSqlSugarDbContext<TDbContext>(this IServiceCollection services,Action<ConnectionConfig> action, LifeTime lifetime = LifeTime.Scope)
        {
            
            ConnectionConfig config=new ConnectionConfig();
            config.ConfigureExternalServices = TableAttributeConfig.AddContextColumsConfiure();
            action.Invoke(config);
            LiftTimeDicInit(config);
            dic[lifetime].Invoke(services);
            return services;
        }

        private static void LiftTimeDicInit(ConnectionConfig connection)
        {
            dic = new ConcurrentDictionary<LifeTime, Action<IServiceCollection>>();         
            dic.TryAdd(LifeTime.Singleton, (service) =>
            {
                ISqlSugarClient sugar = new SqlSugarScope(connection);
                service.AddSingleton<ISqlSugarClient>(sugar);
            });
            dic.TryAdd(LifeTime.Singleton, (service) =>
            {
                service.AddScoped<ISqlSugarClient>(p=>new SqlSugarClient(connection));
            });
        }
    }
}
