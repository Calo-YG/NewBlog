﻿using Calo.Blog.Common.Authorization;
using Calo.Blog.Common.CustomOptions;
using Calo.Blog.Common.Filters;
using Calo.Blog.Common.Redis;
using Calo.Blog.Extenions.AjaxResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using Y.Module;
using Y.Module.Modules;
using Y.Module.Extensions;
using Calo.Blog.Common.UserSession;
using Calo.Blog.Common.Authorization.Authorize;
using Calo.Blog.Common.Y.EventBus.Y.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Calo.Blog.Common.Minio;
using System.Reflection;
using RabbitMQ.Client;
using Mapster;
using MapsterMapper;

namespace Calo.Blog.Common
{
    public class CommonModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            var configuration = context.GetConfiguartion();

            //统一返回值处理工厂
            context.Services.AddScoped<IActionResultWrapFactory, FilterResultWrapFactory>();
            context.Services.AddSingleton<ITokenProvider, TokenProvider>();

            //权限检测程序
            context.Services.AddScoped<IPermissionCheck, PermissionCheck>();
            context.Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationProvider>();
            context.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizeMiddleHandle>();
            context.Services.AddSingleton<IAuthorizationHandler, AuthorizeHandler>();
            //注入IUserSession
            context.Services.AddScoped<IUserSession, CurrentUserSession>();
            //添加权限
            context.Services.AddSingleton<IAuthorizeManager, AuthorizeManager>();

            //使用CsRedis
            var redisSetting = configuration.GetSection("App:RedisSetting").Get<RedisSetting>();
            var csredis = new CSRedis.CSRedisClient(redisSetting.Connstr);
            context.Services.AddSingleton<IDistributedCache>(new CSRedisCache(csredis));
            RedisHelper.Initialization(csredis);

            context.Services.AddSingleton<ICacheManager, CacheManager>();

            context.Services.AddControllers(options =>
            {
                options.Filters.Add<ResultFilter>();
            });

            context.Services.AddRabbitMQ(configuration);

            var minioconfigure = configuration.GetSection("App:MinioConfig")
            .Get<MinioConfig>() ?? throw new NullReferenceException("请设置Minio基础配置");

            context.Services.AddMinio((p) =>
            {
                p.DefaultBucket = minioconfigure.DefaultBucket;
                p.Protal = minioconfigure.Protal;
                p.SecretKey = minioconfigure.SecretKey;
                p.AccessKey = minioconfigure.AccessKey;
                p.Host = minioconfigure.Host;
                p.Password = minioconfigure.Password;
                p.UserName = minioconfigure.UserName;   
            });

            Configure<MinioConfig>(p =>
            {
                p.DefaultBucket = minioconfigure.DefaultBucket;
                p.Protal = minioconfigure.Protal;
                p.SecretKey = minioconfigure.SecretKey;
                p.AccessKey = minioconfigure.AccessKey;
                p.Host = minioconfigure.Host;
                p.Password = minioconfigure.Password;
                p.UserName = minioconfigure.UserName;
            });

            Configure<ExceptionOptions>(p =>
            {
                p.UseDataBase = false;
            });

            ///程序集注入
            context.Services.AddAssembly(Assembly.GetExecutingAssembly());


        }

        public override async Task LaterInitApplicationAsync(InitApplicationContext context)
        {
            var scope = context.ServiceProvider.CreateAsyncScope();

            var minioService = scope.ServiceProvider.GetRequiredService<IMinioService>();

            //await scope.ServiceProvider
            //    .GetRequiredService<IMinioService>()
            //    .CreateDefaultBucket();

            await Task.CompletedTask;
        }

        private void AddMapter(IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            // Or
            // var config = TypeAdapterConfig.GlobalSettings;
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}
