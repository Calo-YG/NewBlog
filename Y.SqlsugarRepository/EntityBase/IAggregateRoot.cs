namespace Y.SqlsugarRepository.EntityBase
{
    public interface IAggregateRoot :IConcurrentToken
    {
           Guid Id { get; }
    }

    public interface IAggregateRoot<TPrimaryKey>:IConcurrentToken
    {
        TPrimaryKey Id { get; } 
    }
}
