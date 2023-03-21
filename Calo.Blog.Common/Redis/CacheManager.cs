

using Microsoft.Extensions.Caching.Distributed;

namespace Calo.Blog.Common.Redis
{
    public class CacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;
        public CacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }
    }
}
