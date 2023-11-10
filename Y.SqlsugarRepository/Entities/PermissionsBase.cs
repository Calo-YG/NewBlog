using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.EntityCore.Entities
{
    public class PermissionsBase:Entity<string>
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string? ParentCode{ get; set; }
        [StringAtrribute(length:40)]
        public string Name { get; set; } 
        public string Code { get; set; }

        public bool Group {  get; set; }
        public bool Page { get; set; }

        public bool Button { get; set; }
    }
}
