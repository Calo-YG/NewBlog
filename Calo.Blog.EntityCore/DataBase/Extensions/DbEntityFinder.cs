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
    select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property?.DeclaringType);
        }
    }
}
