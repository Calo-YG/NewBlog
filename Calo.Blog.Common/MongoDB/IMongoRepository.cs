namespace Calo.Blog.Common.MongoDB
{
    public interface IMongoRepository<T>
    {
        void ChangeDatabase(string databaseName);
    }
}