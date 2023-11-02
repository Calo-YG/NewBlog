using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.EntityCore.Entities
{
    public class PermissionsBase:Entity<Guid>
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string? ParentCode{ get; set; }
        public string Name { get; set; } 

        public string Code { get; set; }
        
        public string? UserId { get; set; }

        public string? RoleId { get; set; }

    }
}
