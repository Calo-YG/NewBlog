namespace Calo.Blog.Common.MongoDB
{
    public interface IMongoUnitOfWork
    {
        Task Transaction(Func<Task> func);
    }
}
