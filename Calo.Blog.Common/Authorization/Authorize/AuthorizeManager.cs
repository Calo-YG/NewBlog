﻿using Calo.Blog.Common.Redis;
using Calo.Blog.EntityCore.Entities;
using CSRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlSugar;
using Y.SqlsugarRepository.DatabaseConext;
using Yitter.IdGenerator;

namespace Calo.Blog.Common.Authorization.Authorize
{
    public class AuthorizeManager : IAuthorizeManager
    {
        private readonly ICacheManager _cacheManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthorizeRegister _authorizeRegister;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public AuthorizeManager(
            ICacheManager cacheManager,
            IConfiguration configuration,
            IAuthorizeRegister authorizeRegister,
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory
        )
        {
            _cacheManager = cacheManager;
            _configuration = configuration;
            _authorizeRegister = authorizeRegister;
            _serviceProvider = serviceProvider;
            _logger = loggerFactory.CreateLogger<IAuthorizeManager>();
        }

        public async Task AddAuthorizeRegiester(IAuthorizePermissionContext Context)
        {
            var current = _cacheManager.Current;
            var key = _configuration.GetSection("App:Permission").Get<string>();
            List<Task<bool>> tasks = new();

            var providers = _authorizeRegister.AuthorizeProviders;
            foreach (var provider in providers)
            {
                provider.PermissionDefined(Context);
            }
            var permissions = InitPermission(Context);
            Context.Dispose();
            try 
            {
                using var scope = _serviceProvider.CreateAsyncScope();
                var _db = scope.ServiceProvider.GetRequiredService<ISqlSugarClient>();
                var context = _db.CreateContext<SugarContext>();
                //插入前删除数据
                await _db.Deleteable<Permissions>().Where("1=1").ExecuteCommandAsync();
                //sqlsuagr 批量插入
                await _db.Insertable(permissions).ExecuteCommandAsync();
                context.Commit();
                var isexists =await RedisHelper.ExistsAsync(key);
                if (isexists)
                {
                    await RedisHelper.SetAsync(key, permissions,-1,RedisExistence.Xx);
                }
                else
                {
                    await RedisHelper.SetAsync(key, permissions, -1, RedisExistence.Nx);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        private List<Permissions> InitPermission(IAuthorizePermissionContext context)
        {
            List<Permissions> permissions = new List<Permissions>();
            Permissions permission;

            foreach (var childer in context.DefinePermission.Childrens)
            {
                permission = new Permissions(
                    YitIdHelper.NextId().ToString(),
                    childer.Name,
                    childer.Code,
                    childer.ParentCode,
                    childer.IsGroup,
                    childer.Page,
                    childer.Button
                );

                permissions.Add(permission);

                InitChilder(permissions, childer.Childrens);

                childer.Dispose();
            }

            return permissions;
        }

        private void InitChilder(List<Permissions> permissions,List<SystemPermission>? systemPermissions)
        {
            if (systemPermissions is null)
            {
                return;
            }
            Permissions permission;
            foreach (var item in systemPermissions)
            {
                permission = new Permissions(
                    YitIdHelper.NextId().ToString(),
                    item.Name,
                    item.Code,
                    item.ParentCode,
                    item.IsGroup,
                    item.Page,
                    item.Button
                );

                permissions.Add(permission);

                InitChilder(permissions, item.Childrens);

                item.Dispose();
            }
        }
    }
}
