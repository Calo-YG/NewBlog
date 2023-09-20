using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.EntityCore.Entities
{
    public class RoleBase:AutiedEntity<Guid>
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}
