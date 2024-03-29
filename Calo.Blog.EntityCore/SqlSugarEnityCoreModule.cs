﻿using Calo.Blog.Domain.Sqlsugarcore;
using Calo.Blog.EntityCore.DadaSeed;
using Calo.Blog.EntityCore.DataBase.Entities;
using Calo.Blog.EntityCore.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Y.Module;
using Y.Module.Extensions;
using Y.Module.Modules;
using Y.SqlsugarRepository;
using Y.SqlsugarRepository.DatabaseConext;
using Y.SqlsugarRepository.Entensions;
using Y.SqlsugarRepository.Entities;
using Y.SqlsugarRepository.Repository;

namespace Calo.Blog.EntityCore
{
    public class SqlSugarEnityCoreModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            var configuration = context.GetConfiguartion();
            context.Services.AddSqlSugarClientAsScope(p =>
            {
                p.ConnectionString = configuration.GetSection("App:ConnectionString:Default").Value;
                p.DbType = SqlSugar.DbType.SqlServer;
                p.IsAutoCloseConnection = true;
                p.ConfigureExternalServices = TableAttributeConfig.AddContextColumsConfigure();
            });
            context.Services.AddUnitOfWorkScoped();
            context.Services.AddSingleton<IEntityManager, EntityManager>();
            //添加数据库上下文AOP配置
            context.Services.AddScoped<IDbAopProvider, DbAopProvider>();
            Configure<DbConfigureOptions>(options =>
            {
                var config = configuration
                .GetSection("App:DbConfigureOptions")
                .Get<DbConfigureOptions>();
                options.EnableAopLog = config.EnableAopLog;
                options.EnableAopError = config.EnableAopError;
            });
            context.Services.AddRepository(provider =>
            {
                //添加数据库实体
                provider.AddEntity<User>();
                provider.AddEntity<Role>();
                provider.AddEntity<Permissions>();
                provider.AddEntity<FreeInterface>();
                provider.AddEntity<ResourceOwner>();
                provider.AddEntity<SourceBucket>();
                provider.AddEntity<Resource>();
                provider.AddEntity<SourceType>();
                provider.AddEntity<OrganizationUnit>();
                provider.AddEntity<OrganizationRole>();
                provider.AddEntity<OrganizationUser>();
                provider.AddEntity<UserPermission>();
                provider.AddEntity<RolePermission>();
            });
            //数据库建库建表配置
            Configure<DatabaseSetting>(p =>
            {
                //跳过建库建表
                p.SikpBuildDatabase = false;
            });
            context.Services.AddAssembly(assembly: Assembly.GetExecutingAssembly());
        }

        public override void LaterInitApplication(InitApplicationContext context)
        {
            var entityManager = context.ServiceProvider
                 .GetRequiredService<IEntityManager>();

            //初始化数据库
            entityManager.BuildDataBase();
            //添加种子数据
            entityManager.DbSeed(async (client) =>
            {
                await new DataBaseSeed(client, context.ServiceProvider).Create();
            });
        }
    }
}
