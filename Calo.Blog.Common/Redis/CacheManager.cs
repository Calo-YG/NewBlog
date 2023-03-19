using FreeRedis;

namespace Calo.Blog.Common.Redis
{
    public class CacheManager:ICacheManager
    {
        private readonly IRedisClient _redisClient;
        public CacheManager(IRedisClient redisClient) 
        { 
           _redisClient= redisClient;
        }   
    }
}
