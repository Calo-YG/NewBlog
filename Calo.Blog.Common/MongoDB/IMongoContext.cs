using MongoDB.Driver;

namespace Calo.Blog.Common.MongoDB
{
    public interface IMongoContext : IDisposable
    {
        void ChangeMongoDatbase(string name);
        Task<IClientSessionHandle> GetSessionHandle();
    }
}
