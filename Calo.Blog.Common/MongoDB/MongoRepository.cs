namespace Calo.Blog.Common.MongoDB
{
    public class MongoRepository<T> :IMongoRepository<T> where T : class
    {
        private readonly IMongoContext _context;
        public MongoRepository(IMongoContext context)
        {
            _context = context; 
        }

        public void ChangeDatabase(string databaseName)
        { 
            _context.ChangeMongoDatbase(databaseName);
        }
    }
}
