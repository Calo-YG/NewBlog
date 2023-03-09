using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public interface IDbEntityFinder
    {
        IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType);
    }
}
