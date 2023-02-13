using Autofac;
using Autofac.Builder;
using Calo.Blog.EntityCore.DataBase.DatabaseContext;
using Calo.Blog.EntityCore.DataBase.EntityBase;
using Calo.Blog.EntityCore.DataBase.Repository;
using Calo.Blog.Extenions.DependencyInjection.AutoFacDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Extensions
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
            foreach (var entity in entities)
            {
                var primaryKeyType = entity.EntityType;
                var genericRepoType = repositoryType.MakeGenericType(entity.EntityType);
                var gerericRepoTypeImpl = repositoryImpl.MakeGenericType(entity.EntityType);
                builder.RegisterType(gerericRepoTypeImpl).As(genericRepoType).InstancePerLifetimeScope();
                if (repositoryTypeWithKey.IsGenericType && repositoryTypeWithKey.GetGenericArguments().Length == 2)
                {
                    var primaryKey = GetPrimaryKeyType(entity.EntityType);
                    var genericeRepoKeyType = repositoryTypeWithKey.MakeGenericType(entity.EntityType, primaryKey);
                    var genericeRepoKeyTypeImpl = repositoryTypeWithKeyImpl.MakeGenericType(entity.EntityType, primaryKey);
                    builder.RegisterType(genericeRepoKeyTypeImpl).As(genericeRepoKeyType).InstancePerLifetimeScope();
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
        
        public static IServiceCollection AddRepository<TDbContext>(this IServiceCollection services) where  TDbContext : BaseContext
        {
            var entities = GetEntityTypeInfo(typeof(TDbContext));
            var defaultType = AutoRegisterRepository.Default;
            var repositoryType = defaultType.RepositoryInterface;
            var repositoryImpl = defaultType.RepositoryImplementation;
            var repositoryTypeWithKey = defaultType.RepositoryInterfaceWithPrimaryKey;
            var repositoryTypeWithKeyImpl = defaultType.RepositoryImplementationWithPrimaryKey;
            using var sp = services.BuildServiceProvider();
            foreach(var entity in entities)
            {
               // var primaryKeyType = entity.EntityType;
                var genericRepoType = repositoryType.MakeGenericType(entity.EntityType);
                var gerericRepoTypeImpl = repositoryImpl.MakeGenericType(entity.EntityType);
                if (sp.IsExistsInDependInjection(genericRepoType))
                {
                    services.AddScoped(genericRepoType, gerericRepoTypeImpl);
                }
                if (repositoryTypeWithKey.IsGenericType && repositoryTypeWithKey.GetGenericArguments().Length == 2)
                {
                    var primaryKey = GetPrimaryKeyType(entity.EntityType);
                    var genericeRepoKeyType = repositoryTypeWithKey.MakeGenericType(entity.EntityType, primaryKey);
                    var genericeRepoKeyTypeImpl = repositoryTypeWithKeyImpl.MakeGenericType(entity.EntityType, primaryKey);
                    if (sp.IsExistsInDependInjection(genericeRepoKeyType))
                    {
                        services.AddScoped(genericeRepoKeyType, genericeRepoKeyTypeImpl);
                    }
                }
            }
            return services;
        }
        private static bool IsExistsInDependInjection(this IServiceProvider provider,Type repotype) {
            var injection = provider.GetService(repotype);    
            return injection is null;
        }
    }
}
