using Calo.Blog.Common.MongoDB.MongoAttribute;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

namespace Calo.Blog.Common.MongoDB
{
    public class MongoContext : IMongoContext
    {
        private readonly MongoOptions _options;
        private readonly IMongoClient _client;
        public IMongoDatabase Current { get; private set;}

        public MongoContext(IOptions<MongoOptions> options)
        {
            _options = options.Value;
            _client = new MongoClient(_options.ConnectString);
            Current = _client.GetDatabase(_options.DataBaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var entity = typeof(T);
            var name = entity.GetCustomAttributes().OfType<TableAttribute>().FirstOrDefault()?.Name ?? entity.Name;
            return Current.GetCollection<T>(name);
        }
        
        public void ChangeMongoDatbase(string name)
        {
            if (_client is null) throw new MongoException("MongoDbClient IS NULL");
            Current = _client.GetDatabase(name);
        }

        public Task<IClientSessionHandle> GetSessionHandle()
        {
            return _client.StartSessionAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
