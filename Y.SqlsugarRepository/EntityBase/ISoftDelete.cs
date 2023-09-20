namespace Y.SqlsugarRepository.EntityBase
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; }
    }
}
