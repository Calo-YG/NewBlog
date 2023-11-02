using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Entities
{
    public class OrganizationUnit:FullAggregarteRoot<Guid>
    {
        public Guid? ParentId { get; set; }

        [StringAtrribute(length:20)]
        public string Name { get; set; }
    }
}
