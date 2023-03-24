using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.EntityCore.Entities
{
    public class PermissionsBase:Entity<long>
    {
        [KeyWithIncrement]
        public new long Id { get; set; }
        public long? ParentId { get; set; }
        public string Name { get; set; } 

        public string Code { get; set; }
        
        public long? UserId { get; set; }

        public long? RoleId { get; set; }

    }
}
