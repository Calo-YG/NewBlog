using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.Entensions;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public class EntityRepositoryInjection : IEntityRepositoryInjection, IEntityContainer
    {
        /// <summary>
        /// 数据库实体类型
        /// </summary>
        public IServiceCollection Services { get; set; }
        /// <summary>
        /// 数据库上下文类型
        /// </summary>
        public IReadOnlyList<Type> EntityTypes { get; set; }

        public EntityRepositoryInjection(IServiceCollection services, IEntityProvider provider)
        {
            Services = services;
            EntityTypes = provider.Entitys;

            services.AddSingleton<IEntityRepositoryInjection>(this);
            services.AddSingleton<IEntityContainer>(this);
        }

        public EntityRepositoryInjection(IServiceCollection services)
        {
            Services = services;
            services.AddSingleton<IEntityRepositoryInjection>(this);
            services.AddSingleton<IEntityContainer>(this);
        }



        /// <summary>
        /// 添加数据库仓储
        /// </summary>
        public virtual void AddRepository()
        {

        }
        /// <summary>
        /// 添加数据库仓储
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        public virtual void AddRepository<TDbContext>() where TDbContext : ISqlSugarClient
        {

        }
        /// <summary>
        /// 获取数据库实体集合
        /// </summary>
        /// <param name="contextType">数据库上下文类型</param>
        /// <returns></returns>
        public virtual Type[] FindEntities(IEntityProvider provider)
        {
            return provider.Entitys;
        }
    }
}
