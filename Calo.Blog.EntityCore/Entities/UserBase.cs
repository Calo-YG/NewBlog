using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.EntityCore.DataBase.Entities
{
    public class UserBase : FullAutiedEntity<long>
    {
        [KeyWithIncrement]
        public new long Id { get; set; }
    }
}
