using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Entities
{
    public class RolePermission:Entity<Guid>
    {
        public Guid RoleId { get; set; }

        public string PermissonCode { get; set; }

        public Guid PermissionId { get; set; }
    }
}
