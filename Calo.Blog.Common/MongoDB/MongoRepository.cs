using MongoDB.Driver;

namespace Calo.Blog.Common.MongoDB
{
    public class MongoRepository<T> :IMongoRepository<T> where T : class
    {
        private readonly IMongoContext _context;
        private readonly IMongoCollection<T> _collection;
        public MongoRepository(IMongoContext context)
        {
            _context = context;
            _collection = _context.GetCollection<T>();
        }

        public Task Insert(T item) 
        {
           return _collection.InsertOneAsync(item);
        }

        public Task BatchInsert(List<T> entitys)
        {
            return _collection.InsertManyAsync(entitys);
        }

        public void ChangeDatabase(string databaseName)
        { 
            _context.ChangeMongoDatbase(databaseName);
        }
    }
}
