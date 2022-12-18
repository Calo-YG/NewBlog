using Calo.Blog.Extenions.DependencyInjection.LifeTimeMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Extensions
{
    public interface IDbEntityFinder : IDependencyInjectionTransit
    {
        IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType);
    }
}
