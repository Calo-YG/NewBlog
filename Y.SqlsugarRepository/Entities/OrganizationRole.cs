using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Entities
{
    public class OrganizationRole:FullAutiedEntity<Guid>
    {
       public Guid RoleId { get; set; }

       public Guid OrganizationId { get; set; }
    }
}
