using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Extensions;
using Y.SqlsugarRepository.Entensions;
using Y.SqlsugarRepository.EntityBase;

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
        /// 导入需要注入的实体
        /// </summary>
        /// <param name="provider"></param>
        public void LoadEntity(IEntityProvider provider)
        {
            EntityTypes = provider.Entitys;
        }
        /// <summary>
        /// 添加数据库仓储
        /// </summary>
        public virtual void AddRepository()
        {
            if (!EntityTypes.Any())
            {
                throw new ApplicationException("没有需要注入的实体,请在ConfigerService方法中使用EntityProvider添加需要注入的实体");
            }
            var defaultType = AutoRegisterRepository.Default;
            var repositoryType = defaultType.RepositoryInterface;
            var repositoryImpl = defaultType.RepositoryImplementation;
            var repositoryTypeWithKey = defaultType.RepositoryInterfaceWithPrimaryKey;
            var repositoryTypeWithKeyImpl = defaultType.RepositoryImplementationWithPrimaryKey;
            foreach (var entity in EntityTypes)
            {
                // var primaryKeyType = entity.EntityType;
                var genericRepoType = repositoryType.MakeGenericType(entity);
                var gerericRepoTypeImpl = repositoryImpl.MakeGenericType(entity);
                if (!Services.IsExists(genericRepoType))
                {
                    Services.AddScoped(genericRepoType, gerericRepoTypeImpl);
                }
                if (repositoryTypeWithKey.IsGenericType && repositoryTypeWithKey.GetGenericArguments().Length == 2)
                {
                    var primaryKey = GetPrimaryKeyType(entity);
                    var genericeRepoKeyType = repositoryTypeWithKey.MakeGenericType(entity, primaryKey);
                    var genericeRepoKeyTypeImpl = repositoryTypeWithKeyImpl.MakeGenericType(entity, primaryKey);
                    if (Services.IsExists(genericeRepoKeyType))
                    {
                        Services.AddScoped(genericeRepoKeyType, genericeRepoKeyTypeImpl);
                    }
                }
            }
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
        /// <summary>
        /// 获取主键类型
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        protected virtual Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            throw new ApplicationException("没有找到实体类型主键: " + entityType + ". 确认实体是否继承了IEntity接口");
        }
    }
}
