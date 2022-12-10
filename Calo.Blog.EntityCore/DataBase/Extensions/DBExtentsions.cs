using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Extensions
{
    public static class DBExtentsions
    {
        public static IServiceCollection AddSqlSugarDbContext<TDbContext>(this IServiceCollection services,Action<ConnectionConfig> action,ServiceLifetime lifetime=ServiceLifetime.Transient)
        {
            ConnectionConfig config=new ConnectionConfig();
            SqlSugarScope scope;
            action.Invoke(config);
            if (lifetime == ServiceLifetime.Scoped)
            {
                services.AddScoped<ISqlSugarClient>(p =>
                {
                    scope= new SqlSugarScope(config);
                    return scope;
                });
            }else if(lifetime == ServiceLifetime.Singleton) 
            {
                services.AddSingleton<ISqlSugarClient>(p =>
                {
                    scope = new SqlSugarScope(config);
                    return scope;
                });
            }
            else
            {
                services.AddTransient<ISqlSugarClient>(p =>
                {
                    scope = new SqlSugarScope(config);
                    return scope;
                });
            }
            return services;
        }
    }
}
