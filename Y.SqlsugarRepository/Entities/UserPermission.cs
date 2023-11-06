using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Entities
{
    public class UserPermission:Entity<Guid>
    {
        public Guid UserId { get; set; }

        public string PermissonCode {  get; set; }

        public string PermissionId { get; set; }
    }
}
