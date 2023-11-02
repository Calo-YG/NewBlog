namespace Y.SqlsugarRepository.EntityBase
{
    public interface IFullAggregateRoot
    {
        Guid Id { get; }    
    }
    
    public interface IFullAggregateRoot<TPrimaryKey>:IEntity<TPrimaryKey>
    {
    }
}
