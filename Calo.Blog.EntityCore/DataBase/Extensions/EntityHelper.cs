using Autofac;
using Calo.Blog.EntityCore.DataBase.EntityBase;
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
        public static void RegisterRepositoyType(this ContainerBuilder builder)
        {

        }

        public static void RegisterRepositoyTypeWithPrimaryKey(this ContainerBuilder builder)
        {

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
            (EntityHelper.IsAssignableToGenericType(property.PropertyType, typeof(ISugarDbSet<>)) ||
                         EntityHelper.IsAssignableToGenericType(property.PropertyType, typeof(SugarDbSet<>))) &&
                        EntityHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
            select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property?.DeclaringType);
        }
    }
}
