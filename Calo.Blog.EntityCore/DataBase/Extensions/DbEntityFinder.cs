using Calo.Blog.EntityCore.DataBase.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Extensions
{
    public class DbEntityFinder : IDbEntityFinder
    {
        public  IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType)
        {
            return
    from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
    where
    (EntityHelper.IsAssignableToGenericType(property.PropertyType, typeof(ISugarDbSet<>)) ||
                 EntityHelper.IsAssignableToGenericType(property.PropertyType, typeof(SugarDbSet<>))) &&
                EntityHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
    select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property?.DeclaringType);
        }
    }
}
