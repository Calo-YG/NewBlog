
using Microsoft.Extensions.DependencyInjection;
using Calo.Blog.Host.Filters;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using Y.Module.Modules;
using Y.Module;
using Y.Module.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using Calo.Blog.EntityCore;
using Calo.Blog.Extenions.AjaxResponse;

namespace Calo.Blog.Host
{
    [DependOn(typeof(SqlSugarEnityCoreModule))]
    public class CaloBlogHostModule : YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            var configuration = context.GetConfiguartion();
            context.Services.AddMvc()
                           .AddRazorPagesOptions(options =>
                           {

                           })
                           .AddRazorRuntimeCompilation();

            context.Services.AddHttpContextAccessor();

            context.Services.AddControllers(options =>
            {
                options.Filters.Add<ResultFilter>();
            });
            //统一返回值处理工厂
            context.Services.AddScoped<IActionResultWrapFactory, FilterResultWrapFactory>();
            context.Services.AddSwaggerGen(options =>
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
        }

        public override void InitApplication(InitApplicationContext context)
        {
            var app = context.GetApplicationBuilder();

            var env = (IHostingEnvironment)context.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endOptions =>
            {
                endOptions.MapDefaultControllerRoute();
                endOptions.MapRazorPages();
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Calo API V1");
                options.EnableDeepLinking();
                options.DocExpansion(DocExpansion.None);
                options.IndexStream = () =>
                {
                    var path = Path.Join(env.WebRootPath, "pages", "swagger.html");
                    return new FileInfo(path).OpenRead();
                };
            });

        }
    }
}
