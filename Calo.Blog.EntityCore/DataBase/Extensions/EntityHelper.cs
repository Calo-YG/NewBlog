using Autofac;
using Calo.Blog.EntityCore.DataBase.DatabaseContext;
using Calo.Blog.EntityCore.DataBase.EntityBase;
using Calo.Blog.EntityCore.DataBase.Repository;
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
            var contextType = assemnblies.Where(p=>p.GetType().BaseType == typeof(BaseContext))
                .FirstOrDefault();
            if(contextType is null)
            {
                throw new ApplicationException("please add a data context");
            }
            return contextType;
        }
        
        public static void AutoRegisterRepositoy(this ContainerBuilder builder)
        {
            var context = ContextFinder();
            var defaultType = AutoRegisterRepository.Default;
            RegisterRepositoyTypeWithPrimaryKey(context, defaultType.RepositoryInterface, defaultType.RepositoryInterfaceWithPrimaryKey);
        }

        public static void RegisterRepositoyTypeWithPrimaryKey(Type dbContext,Type repositoryType , Type repositoryTypeWithKey)
        {
            var implrepository = typeof(BaseRepository<>);//不包含主键仓储的实现
            var implrepositoryWithKey = typeof(BaseRepository<,>);//包含仓储主键的实现
            var entities = GetEntityTypeInfo(dbContext);
            foreach(var entity in entities)
            {
                var primaryKeyType = entity.EntityType;
                var genericRepoType = repositoryType.MakeGenericType(entity.EntityType);

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
