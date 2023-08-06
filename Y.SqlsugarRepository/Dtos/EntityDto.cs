using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Dtos
{
    public class EntityDto<TPrimaryKey> : IConcurrentToken
    {
        public TPrimaryKey Id { get; set; }
        public string? ConcurrentToken { get; set; }
    }
}
