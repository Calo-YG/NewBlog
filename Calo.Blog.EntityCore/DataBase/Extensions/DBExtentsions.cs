using Microsoft.Extensions.DependencyInjection;
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
        public static IServiceCollection AddSqlSugarDbContext<TDbContext>(this IServiceCollection services,Action<ConnectionConfig> action,ServiceLifetime lifetime=ServiceLifetime.Transient) where TDbContext:IDbContext
        {
            ConnectionConfig config=new ConnectionConfig();
            action.Invoke(config);
            if (lifetime == ServiceLifetime.Scoped)
            {
                //services.AddScoped<ISqlSugarClient,SqlSugarScope>(p)；
            }
            return services;
        }
    }
}
