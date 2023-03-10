# Colo.Blog
### 相关模块介绍
Y.Module
模块化类库，参照AbpVnext实现，现已正常使用
[Y.Module](http://https://www.nuget.org/packages/Y.Module)
todo
1.实现批量注入方式
2.DependOn特性待测试
3.待思考
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
        }

        public override void InitApplication(InitApplicationContext context)
        {
            var app = context.GetApplicationBuilder();

            var env = (IHostingEnvironment)context.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            //在这里使用中间件

        }
    }
}

```
### Y.SqlSugarRepository

基于SqlSugar仓储注入类库
- 已实现仓储批量注入（待测试）
- 基于领域驱动设计带实现聚合根
- 待完善仓储注入的扩展方法
- 待完善数据库上下文注入扩展方法
- 待实现统一的事务管理（Aop工作单元）


