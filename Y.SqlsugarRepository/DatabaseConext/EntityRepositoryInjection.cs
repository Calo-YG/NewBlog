using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Y.Module.Extensions;
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
            // 使用 LINQ 来过滤出没有注册的实体类型
            var unregisteredEntities = EntityTypes.Where(entity => !Services.IsExists(repositoryType.MakeGenericType(entity)));

            foreach (var entity in unregisteredEntities)
            {
                // 注册仓储接口和实现
                Services.AddScoped(repositoryType.MakeGenericType(entity), repositoryImpl.MakeGenericType(entity));

                // 如果仓储接口有主键参数，再注册一次
                if (repositoryTypeWithKey.IsGenericType && repositoryTypeWithKey.GetGenericArguments().Length == 2)
                {
                    var primaryKey = GetPrimaryKeyType(entity);
                    Services.AddScoped(repositoryTypeWithKey.MakeGenericType(entity, primaryKey), repositoryTypeWithKeyImpl.MakeGenericType(entity, primaryKey));
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

            throw new ApplicationException("没有找到实体类型主键: " + entityType.Name + ". 确认实体是否继承了IEntity接口");
        }
    }
}
