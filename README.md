# Colo.Blog

#### 该项目已通过阿里云云效、GithubAction持续集成与部署（[Home Page - Calo.Blog.Host](http://124.71.15.19:1545/)）

## 相关模块介绍
### Y.Module

#### 模块化类库，参照AbpVnext实现，现已正常使用
[Y.Module](http://https://www.nuget.org/packages/Y.Module)
todo
1.实现批量注入方式（待完成（已完成特性批量注入），基于接口的批量注入带实现）

#### 使用方式
```c#

namespace Calo.Blog.Host
{
    public class CaloBlogHostModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            var configuration = context.GetConfiguartion();
            //在这里注入你的服务
            //批量注入
             context.Services.AddAssembly(assembly: Assembly.GetExecutingAssembly());
        }

        public override void InitApplication(InitApplicationContext context)
        {
            var app = context.GetApplicationBuilder();

            var env = (IHostingEnvironment)context.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            //在这里使用中间件
           
        }
    }
}

//异步初始化
public override async Task LaterInitApplicationAsync(InitApplicationContext context)
{
     var scope = context.ServiceProvider.CreateAsyncScope();

     var minioService = scope.ServiceProvider.GetRequiredService<IMinioService>();

     //await scope.ServiceProvider
     //    .GetRequiredService<IMinioService>()
     //    .CreateDefaultBucket();

     await Task.CompletedTask;
}

```
特性批量注入

```c#
//生命周期枚举，是否使用接口，接口类型，注意当是否使用接口为true时，必须添加接口类型
//不使用接口类型，直接注入当前类
[Injection(InjectionEnum.Scoped,true,typeof(ITestInject))]
    public class TestInject : ITestInject
    {
        private readonly ILogger<TestInject> _logger;
        public TestInject(ILogger<TestInject> logger)
        {
            _logger = logger;
        }

        public void LogInfo()
        {
            _logger.LogInformation("注入成功");
        }
    }
```
```c#
//接口自动注入，ISignletonInjection,IScopedInjection,ITrasinInjection
public class MinioService:IMinioService,IScopedInjection
{

}
```
```c#
//使用
context.Services.AddAssembly(Assembly.GetExecutingAssembly());
```

### Y.SqlSugarRepository

```c#
        public override void ConfigerService(ConfigerServiceContext context)
        {
            //数据库配置
            // base.ConfigerService(context);
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
            });
            //数据库建库建表配置
            Configure<DatabaseSetting>(p =>
            {
                //跳过建库建表
                p.SikpBuildDatabase = true;
            });
        }

         public override void LaterInitApplication(InitApplicationContext context)
        {
           var entityManager = context.ServiceProvider
                .GetRequiredService<IEntityManager>();

            //初始化数据库
            entityManager.BuildDataBase();
        }

        //使用
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //依赖注入
        private readonly IBaseRepository<User, long> userRespo;

        public HomeController(ILogger<HomeController> logger, IBaseRepository<User, long> baseRepository)
        {
            _logger = logger;
            userRespo = baseRepository;
        }
    }     

```

```json
```



基于SqlSugar仓储注入类库

- 已实现仓储批量注入（已测试）
- 建库建表（已测试）
- 创建种子数据（已测试）
- 基于领域驱动设计带实现聚合根（待实现）
- 待完善仓储注入的扩展方法（已测试）
- 待完善数据库上下文注入扩展方法（已测试）
- 待实现统一的事务管理（Aop工作单元）(待实现)
- 待实现仓储的实现，可以自定义自己的仓储(待实现)

