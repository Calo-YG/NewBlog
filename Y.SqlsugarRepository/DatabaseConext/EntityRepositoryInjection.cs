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
        public IReadOnlyList<EntityTypeInfo> EntityTypes { get; set; }
        public IServiceCollection Services { get; set; }
        /// <summary>
        /// 数据库上下文类型
        /// </summary>
        private Type ContextType { get; set; }
        public EntityRepositoryInjection(IServiceCollection services, Type contextType)
        {
            Services = services;
            EntityTypes = FindEntities(contextType);
            ContextType = contextType;

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
        protected virtual EntityTypeInfo[] FindEntities(Type contextType)
        {
            return new DbEntityFinder().GetEntityTypeInfos(contextType).ToArray();
        }
    }
}
