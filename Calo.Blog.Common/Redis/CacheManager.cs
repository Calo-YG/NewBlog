using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Calo.Blog.Common.Redis
{
    public class CacheManager : ICacheManager
    {
        public CSRedisClient Current { get => _client ?? GetDefaultClient(); }
        private readonly IDistributedCache _cache;
        private readonly CSRedisClient _client;
        private readonly IConfiguration _configuration;
        public CacheManager(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
            _client = RedisHelper.Instance ?? GetDefaultClient();
        }

        private CSRedisClient GetDefaultClient()
        {
            var connstr = _configuration.GetSection("App:redis:connstr").Get<string>();
            var redis = new CSRedisClient(connstr);
            RedisHelper.Initialization(redis);
            return redis;
        }

        private DistributedCacheEntryOptions CreateDistributedOptions(int slider, int absolute)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            slider = Random.Shared.Next(slider, absolute);
            options.SlidingExpiration = TimeSpan.FromSeconds(slider);
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(absolute);
            return options;
        }

        public virtual async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> func, int absulote, int slider)
        {
            var options = CreateDistributedOptions(slider, absulote);
            var value = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(value))
            {
                var val = await func.Invoke();
                val = val ?? default(T);
                value = JsonSerializer.Serialize(val);
                await _cache.SetStringAsync(key, value, options);
            }
            await _cache.RefreshAsync(key);
            return JsonSerializer.Deserialize<T>(value);
        }
    }
}
