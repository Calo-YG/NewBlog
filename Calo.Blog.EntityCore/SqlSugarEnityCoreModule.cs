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
            });
            //数据库建库建表配置
            Configure<DatabaseSetting>(p =>
            {
                //跳过建库建表
                p.SikpBuildDatabase = true;
            });
            context.Services.AddAssembly(assembly: Assembly.GetExecutingAssembly());
        }

        public override void LaterInitApplication(InitApplicationContext context)
        {
            var entityManager = context.ServiceProvider
                 .GetRequiredService<IEntityManager>();

            //初始化数据库
            entityManager.BuildDataBase();
        }
    }
}
