using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Dtos
{
    public class InputBase<TPrimaryKey> : IConcurrentToken
    {
        public TPrimaryKey Id { get; set; }
        public string? ConcurrentToken { get; set; }
    }
}
