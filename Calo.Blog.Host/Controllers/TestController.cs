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
using Y.EventBus;
using Calo.Blog.Application.ResourceOwnereServices.Etos;
using Calo.Blog.Common.Minio;
using Minio;
using SqlSugar;
using Y.SqlsugarRepository.DatabaseConext;
using Calo.Blog.Common.Excetptions;
using Y.SqlsugarRepository.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

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
        private readonly ILocalEventBus _localEventBus;
        private readonly IMinioService _minioService;
        private readonly MinioClient _minioClient;
        private readonly ISqlSugarClient Context;
        private readonly IBaseRepository<User, Guid> _userRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TestController(
            IHttpContextAccessor httpContextAccessor,
            ITokenProvider tokenProvider,
            IDistributedCache cache,
            ICacheManager cacheManager,
            IUserSession userSession,
            ILocalEventBus localEventBus,
            IMinioService minioService,
            MinioClient minioClient,
            ISqlSugarClient context,
            IBaseRepository<User, Guid> userRepository,
            IWebHostEnvironment hostEnvironment
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenProvider = tokenProvider;
            _cache = cache;
            _cacheManager = cacheManager;
            _usersession = userSession;
            _localEventBus = localEventBus;
            _minioService = minioService;
            _minioClient = minioClient;
            Context = context;
            _userRepository = userRepository;
            _hostEnvironment = hostEnvironment;
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
            var result = await context.AuthenticateAsync("Cookies");
            return new User { };
        }

        [HttpGet]
        public async Task<dynamic> GetToken()
        {
            using var uow = Context.CreateContext<SugarContext>(true);
            await uow.DbSet<User>().CountAsync(p => p.CreationTime <= DateTime.Now);

            UserTokenModel tokenModel = new UserTokenModel();
            tokenModel.UserName = "test";
            tokenModel.UserId = "a4281793-35c9-4eeb-bdfe-0dcf9524b59f";
            var token = _tokenProvider.GenerateToken(tokenModel);

            Response.Cookies.Append("x-access-token", token.Token);
            Response.Cookies.Append("refresh-token", token.RefreshToken);
            return new { Token = token.Token, RefreshToken = token.RefreshToken };
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
                var t = _cacheManager.Current.HSetAsync(
                    "wygset",
                    "wyg" + current.ToString(),
                    current
                );
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
            return await _cacheManager.GetOrCreateAsync<User>(
                "user1",
                () => Task.FromResult(user),
                200,
                150
            );
        }

        [HttpGet]
        public string? Info()
        {
            _usersession.SetUserInfo();
            return _usersession.UserId;
        }

        [HttpGet]
        public async Task TestLocalEventBus()
        {
            TestEto eto = null;

            for (var i = 0; i < 100; i++)
            {
                eto = new TestEto()
                {
                    Name = "LocalEventBus" + i.ToString(),
                    Description = "wyg" + i.ToString(),
                };
                await _localEventBus.PublichAsync(eto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFile()
        {
            var obj = new GetObjectInput()
            {
                ObjectName = "YChat_Minio_1989422021_4fcbb43b8b31308f31be02df6bee77ee.jpeg",
                BucketName = "y.chat"
            };

            var output = await _minioService.GetObjectAsync(obj);

            //output.Stream.CopyTo(Console.OpenStandardOutput());

            return new FileStreamResult(output.Stream, output.ContentType);
        }

        [HttpGet]
        public async Task SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet]
        public async Task TestUow()
        {
            using var uow = Context.CreateContext<SugarContext>();
            var user = new User();
            user.BirthDate = DateTime.Now;
            user.Email = "31645222062@qq.com";
            user.Password = "wyg154511";
            user.UserName = "wyg文eee9";
            var isExists = await _userRepository.AnyAsync(p => p.UserName.Equals(user.UserName));
            if (!isExists)
            {
                await _userRepository.InsertAsync(user);
            }
            throw new UserfriednlyException("ddd");
            uow.Commit();
        }

        [Authorize]
        [HttpPost]
        public async Task TestUplock()
        {
            using var uow = Context.CreateContext<SugarContext>();
            var user = await uow.DbSet<User>().FirstAsync();
            user.UserName = "ddd-wyg";
            await _userRepository.UpdateAsync(user, null);
            uow.Commit();
        }

        [Authorize]
        [HttpDelete]
        public async Task TestDelete()
        {
            using var uow = Context.CreateContext<SugarContext>();
            var user = await uow.DbSet<User>().FirstAsync();
            await _userRepository.DeleteAsync(user);
            uow.Commit();
        }

        [HttpPost]
        public async Task UploadVideo()
        {
            var filepath = Path.Join($"{_hostEnvironment.WebRootPath}", "file", "text.mp4");
            var file = new FileInfo(filepath);
            var suffix = file.Name.Split('.');
            var fileProvider = new FileExtensionContentTypeProvider();
            string contentType;
            var getContent = fileProvider.TryGetContentType(file.Name,out contentType);

            using FileStream fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];

            fileStream.Read(bytes, 0, bytes.Length);

            fileStream.Close();

            using MemoryStream ms = new MemoryStream(bytes);
            var obj = new UploadObjectInput("y.chat", file.Name, contentType, ms);

            await _minioService.UploadObjectAsync(obj);
        }
    }
}
