using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Extensions;
using Y.SqlsugarRepository.DatabaseConext;
using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Entensions
{
    public static class EntityHelper
    {
        public static Type ContextFinder()
        {
            var assemnblies = Assembly.GetExecutingAssembly().GetTypes();
            var contextType = assemnblies.Where(p => p.BaseType == typeof(BaseContext))
                .FirstOrDefault();
            if (contextType is null)
            {
                throw new ApplicationException("please add a data context");
            }
            return contextType;
        }

      

        public static Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            throw new ApplicationException("Can not find primary key type of given entity type: " + entityType + ". Be sure that this entity type implements IEntity<TPrimaryKey> interface");
        }

        /// <summary>
        /// Checks whether <paramref name="givenType"/> implements/inherits <paramref name="genericType"/>.
        /// </summary>
        /// <param name="givenType">Type to check</param>
        /// <param name="genericType">Generic type</param>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var givenTypeInfo = givenType.GetTypeInfo();

            if (givenTypeInfo.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenTypeInfo.BaseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(givenTypeInfo.BaseType, genericType);
        }
        /// <summary>
        /// 获取需要注入的实体
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static IEnumerable<EntityTypeInfo> GetEntityTypeInfo(Type dbType)
        {
            return from property in dbType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   where
                   (IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>)) ||
                    IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))) &&
                    IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
                   select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property?.DeclaringType);
        }

        public static IServiceCollection AddRepository<TDbContext>(this IServiceCollection services) where TDbContext : BaseContext
        {
            var entities = GetEntityTypeInfo(typeof(TDbContext));
            var defaultType = AutoRegisterRepository.Default;
            var repositoryType = defaultType.RepositoryInterface;
            var repositoryImpl = defaultType.RepositoryImplementation;
            var repositoryTypeWithKey = defaultType.RepositoryInterfaceWithPrimaryKey;
            var repositoryTypeWithKeyImpl = defaultType.RepositoryImplementationWithPrimaryKey;
            using var sp = services.BuildServiceProvider();
            foreach (var entity in entities)
            {
                // var primaryKeyType = entity.EntityType;
                var genericRepoType = repositoryType.MakeGenericType(entity.EntityType);
                var gerericRepoTypeImpl = repositoryImpl.MakeGenericType(entity.EntityType);
                if (services.IsExists(genericRepoType))
                {
                    services.AddScoped(genericRepoType, gerericRepoTypeImpl);
                }
                if (repositoryTypeWithKey.IsGenericType && repositoryTypeWithKey.GetGenericArguments().Length == 2)
                {
                    var primaryKey = GetPrimaryKeyType(entity.EntityType);
                    var genericeRepoKeyType = repositoryTypeWithKey.MakeGenericType(entity.EntityType, primaryKey);
                    var genericeRepoKeyTypeImpl = repositoryTypeWithKeyImpl.MakeGenericType(entity.EntityType, primaryKey);
                    if (services.IsExists(genericeRepoKeyType))
                    {
                        services.AddScoped(genericeRepoKeyType, genericeRepoKeyTypeImpl);
                    }
                }
            }
            return services;
        }
        private static bool IsExistsInDependInjection(this IServiceProvider provider, Type repotype)
        {
            var injection = provider.GetService(repotype);
            return injection is null;
        }
    }
}
