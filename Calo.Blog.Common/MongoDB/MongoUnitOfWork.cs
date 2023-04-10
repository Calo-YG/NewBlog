using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Calo.Blog.Common.MongoDB
{
    public class MongoUnitOfWork:IMongoUnitOfWork
    {
        private readonly IMongoContext _context;
        private readonly ILogger _logger;

        public MongoUnitOfWork(IMongoContext context,ILoggerFactory factory)
        { 
           _context= context;
           _logger= factory.CreateLogger<MongoUnitOfWork>();
        }
        public async Task Transaction(Func<Task> func)
        {
            IClientSessionHandle? session = await _context.GetSessionHandle();
            session.StartTransaction();
            try
            {
                await func();
                await session.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                await session.AbortTransactionAsync();
                throw;
            }
            finally
            {
                session.Dispose();
            }
        }
    }
}