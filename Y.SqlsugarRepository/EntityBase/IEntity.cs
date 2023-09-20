namespace Y.SqlsugarRepository.EntityBase
{
    public interface IEntity<TPrimarykey>
    {
        TPrimarykey Id {get;set;}
    }

    public interface IEnity
    {
        Guid Id { get;set;}
    }
}
