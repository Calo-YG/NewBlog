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

namespace Calo.Blog.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenProvider _tokenProvider;
        public TestController(IHttpContextAccessor httpContextAccessor, ITokenProvider tokenProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenProvider = tokenProvider;
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


    }
}
