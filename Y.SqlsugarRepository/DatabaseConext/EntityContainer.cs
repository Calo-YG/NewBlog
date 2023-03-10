using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public class EntityContainer : IEntityContainer
    {
        public IReadOnlyList<Type> EntityTypes { get; set; }

        public EntityContainer(List<Type> entityTypes)
        {
            EntityTypes = entityTypes;
        }
    }
}
