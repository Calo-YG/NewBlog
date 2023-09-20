using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Calo.Blog.Common.Redis
{
    public static class RedisExtensions
    {
        public static void AddRedis(this IServiceCollection services,IConfiguration configuration)
        {
            //使用CsRedis
            var redisSetting = configuration.GetSection("App:RedisSetting").Get<RedisSetting>();

            if(redisSetting is null)
            {
                return;
            }

            var csredis = new CSRedis.CSRedisClient(redisSetting.Connstr);

            services.AddSingleton<IDistributedCache>(new CSRedisCache(csredis));

            RedisHelper.Initialization(csredis);

            services.AddSingleton<ICacheManager, CacheManager>();
        }
                
    }
}
