

using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Calo.Blog.Common.Redis
{
    public class CacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;
        public CacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }

        private DistributedCacheEntryOptions CreateDistributedOptions(int slider, int absolute)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            double perfix = slider % absolute;
            if (perfix > 1)
            {
                perfix = Math.Floor(perfix);
                if (perfix < 0.4)
                {
                    perfix += 0.1;
                }
            }
            slider = (int)(perfix * absolute);
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
