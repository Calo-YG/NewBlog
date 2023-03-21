using CSRedis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Redis
{
    public interface ICacheManager
    {
        public CSRedisClient Current { get; }
        Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> func, int absulote, int slider);
    }
}
