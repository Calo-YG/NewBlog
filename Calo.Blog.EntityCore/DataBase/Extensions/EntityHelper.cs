using Autofac;
using Autofac.Builder;
using Calo.Blog.EntityCore.DataBase.DatabaseContext;
using Calo.Blog.EntityCore.DataBase.EntityBase;
using Calo.Blog.EntityCore.DataBase.Repository;
using Calo.Blog.Extenions.DependencyInjection.AutoFacDependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Extensions
{
    public static class EntityHelper
    {
        public static Type ContextFinder()
        {
            var assemnblies = Assembly.GetExecutingAssembly().GetTypes();
            var contextType = assemnblies.Where(p => p.GetType().BaseType == typeof(BaseContext))
                .FirstOrDefault();
            if (contextType is null)
            {
                throw new ApplicationException("please add a data context");
            }
            return contextType;
        }

        public static void AutoRegisterRepositoy(this ContainerBuilder builder)
        {
            if (builder is null)
            {
                throw new ApplicationException("this container is null");
            }
            var context = ContextFinder();
            var defaultType = AutoRegisterRepository.Default;
            RegisterRepositoyTypeWithPrimaryKey(context, defaultType, builder);
        }

        public static void RegisterRepositoyTypeWithPrimaryKey(Type dbContext, RepositoryDependencyInjection defaultType, ContainerBuilder builder)
        {
            var repositoryType = defaultType.RepositoryInterface;
            var repositoryImpl = defaultType.RepositoryImplementation;
            var repositoryTypeWithKey = defaultType.RepositoryInterfaceWithPrimaryKey;
            var repositoryTypeWithKeyImpl = defaultType.RepositoryImplementationWithPrimaryKey;
            var entities = GetEntityTypeInfo(dbContext);
            using (var scope = builder.Build().BeginLifetimeScope())
            {
                foreach (var entity in entities)
                {
                    var primaryKeyType = entity.EntityType;
                    var genericRepoType = repositoryType.MakeGenericType(entity.EntityType);
                    var gerericRepoTypeImpl = repositoryImpl.MakeGenericType(entity.EntityType);
                    if (scope.Resolve(genericRepoType) is null)
                    {
                        builder.RegisterType(gerericRepoTypeImpl).As(genericRepoType).InstancePerLifetimeScope();
                    }
                    if (repositoryTypeWithKey.IsGenericType && repositoryTypeWithKey.GenericTypeArguments.Length == 2)
                    {
                        var primaryKey = GetPrimaryKeyType(entity.EntityType);
                        var genericeRepoKeyType = repositoryTypeWithKey.MakeGenericType(entity.EntityType, primaryKey);
                        var genericeRepoKeyTypeImpl = repositoryTypeWithKeyImpl.MakeGenericType(entity.EntityType, primaryKey);
                        if (scope.Resolve(genericeRepoKeyType) is null)
                        {
                            builder.RegisterType(genericeRepoKeyTypeImpl).As(genericeRepoKeyType).InstancePerLifetimeScope();
                        }
                    }
                }
            }
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
                   (IsAssignableToGenericType(property.PropertyType, typeof(ISugarDbSet<>)) ||
                    IsAssignableToGenericType(property.PropertyType, typeof(SugarDbSet<>))) &&
                    IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
                   select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property?.DeclaringType);
        }
    }
}
