using Y.SqlsugarRepository.EntityAttribute;

namespace Y.SqlsugarRepository.EntityBase
{
    public class AggregateRoot : IAggregateRoot
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        [ConcurrentToken]
        public string? ConcurrentToken { get; set; }
    }

    public class AggregateRootM<TPrimaryKey> : IAggregateRoot<TPrimaryKey>
    {
        [PrimaryKey]
        public TPrimaryKey Id { get; set; }
        [ConcurrentToken]
        public string? ConcurrentToken { get; set; }
    }
}
