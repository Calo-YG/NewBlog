using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public class EntityContainer : IEntityContainer
    {
        public IReadOnlyList<EntityTypeInfo> EntityTypes { get; set; }

        public EntityContainer(IReadOnlyList<EntityTypeInfo> entityTypes)
        {
            EntityTypes = entityTypes;
        }
    }
}
