using SqlSugar;
using Y.SqlsugarRepository.EntityAttribute;

namespace Y.SqlsugarRepository.EntityBase
{
    public class Entity<TPrimarykey> : IEntity<TPrimarykey>, IConcurrentToken
    {
        [PrimaryKey]
        public TPrimarykey Id { get; set; }

        [ConcurrentToken]
        public string? ConcurrentToken { get; set; }
    }

    public class Entity:IEnity
    {
        [PrimaryKey]
        public Guid Id { get; set; }

        [ConcurrentToken]
        public string? ConcurrentToken { get; set; }
    }
}
