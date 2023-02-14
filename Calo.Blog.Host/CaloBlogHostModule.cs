using Calo.Blog.EntityCore.DataBase.Extensions;
using Calo.Blog.EntityCore.DataBase.Repository;
using Calo.Blog.EntityCore.DataBase;
using Calo.Blog.Extenions.AjaxResponse;
using Calo.Blog.Extenions.AppModule;
using Microsoft.Extensions.DependencyInjection;
using Calo.Blog.Host.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;

namespace Calo.Blog.Host
{
    public class CaloBlogHostModule : YModule
    {
        public override void ServiceConfiguration(ServiceConfigurationContext context)
        {
            var configuration = context.Configuration();
            context.Service.AddMvc()
                           .AddRazorPagesOptions(options =>
                           {

                           })
                           .AddRazorRuntimeCompilation();

            context.Service.AddHttpContextAccessor();

            context.Service.AddControllers(options =>
            {
                options.Filters.Add<ResultFilter>();
            });

            context.Service.AddSqlSugarClientAsCleint(p =>
            {
                p.ConnectionString = configuration.GetSection("App:ConnectionString:Default").Value;
                p.DbType = SqlSugar.DbType.SqlServer;
                p.IsAutoCloseConnection = true;
            }).AddSuagarDbContextAsScoped<BlogContext>();

            context.Service.AddScoped<IActionResultWrapFactory, FilterResultWrapFactory>();

            context.Service.AddScoped<IDbAopProvider, DbAopProvider>();
            //添加数据库上下文AOP配置
            context.Service.Configure<DbConfigureOptions>(options =>
            {
                var config =configuration
                .GetSection("App:DbConfigureOptions")
                .Get<DbConfigureOptions>();
                options.EnableAopLog = config.EnableAopLog;
                options.EnableAopError = config.EnableAopError;
            });

            context.Service.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "Calo API v1",
                    Title = "Calo API",
                    Description = "Web API for managing By Calo-YG",
                    TermsOfService = new Uri("https://gitee.com/wen-yaoguang"),
                    Contact = new OpenApiContact
                    {
                        Name = "Gitee 地址",
                        Url = new Uri("https://gitee.com/wen-yaoguang/Colo.Blog")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "个人博客",
                        Url = new Uri("https://www.se.cnblogs.com/lonely-wen/")
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.OrderActionsBy(o => o.RelativePath);
            });

            context.Service.AddRepository<BlogContext>();
        }
    }
}
