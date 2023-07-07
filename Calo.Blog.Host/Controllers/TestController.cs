using Calo.Blog.Common.Authorization;
using Calo.Blog.EntityCore.DataBase.Entities;
using Calo.Blog.Extenions.Attributes;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Generic;
using System.Linq;
using Calo.Blog.Common.Redis;
using Calo.Blog.Common.UserSession;
using Calo.Blog.Common.Y.EventBus;
using Calo.Blog.Domain.Eto;

namespace Calo.Blog.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenProvider _tokenProvider;
        private readonly IDistributedCache _cache;
        private readonly ICacheManager _cacheManager;
        private readonly IUserSession _usersession;
        private readonly IEventBus _eventBus;
        public TestController(IHttpContextAccessor httpContextAccessor
            , ITokenProvider tokenProvider
            , IDistributedCache cache
            , ICacheManager cacheManager
            , IUserSession userSession
            , IEventBus eventBus)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenProvider = tokenProvider;
            _cache = cache;
            _cacheManager = cacheManager;
            _usersession = userSession;
            _eventBus = eventBus;
        }
        /// <summary>
        /// justTestApi
        /// </summary>
        [CustomAuthorization]
        [HttpGet]
        [NoResult]
        public int SetNotOP()
        {
            throw new ArgumentNullException(nameof(TestTask));
            return 1;
        }

        [CustomAuthorization("test1", "test1")]
        [HttpGet]
        public async Task<int> TestTask()
        {
            await Task.CompletedTask;
            return 1;
        }
        [CustomAuthorization("tttt")]
        [HttpGet]
        public async Task<User> GetUser(Guid id)
        {
            var context = _httpContextAccessor.HttpContext;
            var result =await context.AuthenticateAsync("Cookies");
            return new User { Id = 1, };
        }
        [HttpGet]
        public async Task<string> GetToken()
        {
            UserTokenModel tokenModel = new UserTokenModel();
            tokenModel.UserName = "test";
            tokenModel.UserId = 1;
            var token = _tokenProvider.GenerateToken(tokenModel);

            Response.Cookies.Append("x-access-token", token);
            var claimsIdentity = new ClaimsIdentity(tokenModel.Claims, "Login"); 
            AuthenticationProperties properties = new AuthenticationProperties();
            properties.AllowRefresh = false;
            properties.IsPersistent = true;
            properties.IssuedUtc = DateTimeOffset.UtcNow;
            properties.ExpiresUtc = DateTimeOffset.Now.AddMinutes(1);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),properties);
            return token;
        }
        [HttpGet]
        public async Task<IEnumerable<int>> SetArray()
        {
            List<Task<bool>> tlist = new List<Task<bool>>();
            int max = 100;
            int current = 1;
            var date1 = DateTime.Now;
            var exists = await _cacheManager.Current.ExistsAsync("wygset");
            while (current < max && !exists)
            {
                current += 1;
                var t = _cacheManager.Current.HSetAsync("wygset", "wyg" + current.ToString(), current);
                //var t = _cacheManager.Current.SetAsync("current" + current, current,10000,CSRedis.RedisExistence.Nx);
                tlist.Add(t);
                if (tlist.Count > 20)
                {
                    await Task.WhenAll(tlist);
                    tlist.Clear();
                }
            }
            var date2 = DateTime.Now;
            Console.WriteLine("耗时" + date2.Subtract(date1).TotalMilliseconds);
            var dic = new Dictionary<string, int>();
            return dic.Select(p => p.Value);
        }
        [HttpGet]
        public async Task<User?> TestCache()
        {
            User user = new();
            user.UserName = "test";
            user.Phone = "1";
            user.UpdateTime = DateTime.Now;
            user.Id = 1;
            return await _cacheManager.GetOrCreateAsync<User>("user1", () => Task.FromResult(user), 200, 150);
        }
        [HttpGet]
        public long? Info()
        {
            _usersession.SetUserInfo();
            return _usersession.UserId;
        }

        [HttpGet]
        public void EventBusTest()
        {
            _eventBus.Publish<User>(new User());
        }
    }
}
