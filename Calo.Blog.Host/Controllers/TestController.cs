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
        public TestController(IHttpContextAccessor httpContextAccessor
            , ITokenProvider tokenProvider
            , IDistributedCache cache
            , ICacheManager cacheManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenProvider = tokenProvider;
            _cache = cache;
            _cacheManager = cacheManager;
        }
        /// <summary>
        /// justTestApi
        /// </summary>
        [HttpGet("SetNotOP")]
        [NoResult]
        public int SetNotOP()
        {
            return 1;
        }

        [CustomAuthorization("test1", "test1")]
        [HttpGet("TestTask")]
        public async Task<int> TestTask()
        {
            await Task.CompletedTask;
            return 1;
        }
        [CustomAuthorization("tttt")]
        [HttpGet("GetUser")]
        public User GetUser(Guid id)
        {
            return new User { Id = 1, };
        }
        [HttpGet("GetToken")]
        public async Task<string> GetToken()
        {
            UserTokenModel tokenModel = new UserTokenModel();
            tokenModel.UserName = "test";
            tokenModel.UserId = 1;
            var token = _tokenProvider.GenerateToken(tokenModel);

            Response.Cookies.Append("x-access-token", token);
            var claimsIdentity = new ClaimsIdentity(tokenModel.Claims, "Login");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return token;
        }
        [HttpGet("SetArray")]
        public IEnumerable<int> SetArray()
        {
            var list = new List<string>();
            int max = 100;
            int current = 0;
            while (current <= max)
            {
                current += 1;
                list.Add(current.ToString());
                //RedisHelper.HSet("wygset", "wyg" + current.ToString(), current);
                _cache.SetString(current.ToString() + "test", current.ToString());
            }
            var dic = RedisHelper.HGetAll<int>("wygset");
            return dic.Select(p => p.Value);
        }
        [HttpGet("TestCache")]
        public async Task<User?> TestCache()
        {
            User user = new();
            user.UserName = "test";
            user.Phone = "1";
            user.UpdateTime = DateTime.Now;
            user.Id = 1;           
            return await _cacheManager.GetOrCreateAsync<User>("user1", () => Task.FromResult(user), 200, 150);
        }
    }
}
