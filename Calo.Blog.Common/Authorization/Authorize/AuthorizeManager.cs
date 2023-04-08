using Calo.Blog.Common.Redis;
using Microsoft.Extensions.Configuration;

namespace Calo.Blog.Common.Authorization.Authorize
{
    public class AuthorizeManager : IAuthorizeManager
    {
        private ICacheManager _cacheManager;
        private IConfiguration _configuration;

        public AuthorizeManager(ICacheManager cacheManager,IConfiguration configuration) 
        {
          _cacheManager= cacheManager;
          _configuration= configuration;
        }
        public async Task AddAuthorizeRegiester()
        {
            var permissions = AuthorizeRegister.Permissions;
            var current = _cacheManager.Current;
            var key = _configuration.GetSection("App:Permission").Get<string>();
            List<Task<bool>> tasks = new ();    
            if(await current.ExistsAsync(key))
            {
                await current.DelAsync(key);
            }
            foreach(var permission in permissions)
            {
                var task = current.HSetAsync(key, permission.Name, permission);
                tasks.Add(task);
                if (tasks.Count > 25)
                {
                    await Task.WhenAll(tasks);
                }
                tasks.Clear();
            }
        }
    }
}
