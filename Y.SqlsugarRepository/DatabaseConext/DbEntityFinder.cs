using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.Entensions;
using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public class DbEntityFinder
    {
        public IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType)
        {
            return
    from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
    where
    (EntityHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) ||
                 EntityHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))) &&
                EntityHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
    select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property?.DeclaringType);
        }
    }
}
