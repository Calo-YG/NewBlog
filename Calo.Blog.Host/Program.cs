using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Calo.Blog.EntityCore;
using Calo.Blog.EntityCore.DataBase;
using Calo.Blog.EntityCore.DataBase.Extensions;
using Calo.Blog.EntityCore.DataBase.Repository;
using Calo.Blog.Extenions.AjaxResponse;
using Calo.Blog.Extenions.AppModule;
using Calo.Blog.Host;
using Calo.Blog.Host.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc()
    .AddRazorPagesOptions(options =>
    {

    })
    .AddRazorRuntimeCompilation();


builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResultFilter>();
});

builder.Services.AddSqlSugarClientAsCleint(p =>
{
    p.ConnectionString = builder.Configuration.GetSection("App:ConnectionString:Default").Value;
    p.DbType = SqlSugar.DbType.SqlServer;
    p.IsAutoCloseConnection = true;
}).AddSuagarDbContextAsScoped<BlogContext>();

builder.Services.AddTransient<IActionResultWrapFactory, FilterResultWrapFactory>();

builder.Services.AddScoped<IDbAopProvider, DbAopProvider>();
//添加数据库上下文AOP配置
builder.Services.Configure<DbConfigureOptions>(options =>
{
    var config = builder.Configuration
    .GetSection("App:DbConfigureOptions")
    .Get<DbConfigureOptions>();
    options.EnableAopLog = config.EnableAopLog;
    options.EnableAopError = config.EnableAopError;
});

builder.Services.AddSwaggerGen(options =>
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
            Url = new Uri("https://www.cnblogs.com/lonely-wen/")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.OrderActionsBy(o => o.RelativePath);
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule<EntityCoreModuleRegister>();
});

builder.Services.LoadModule<CaloBlogHostModule>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
        var path = Path.Join(builder.Environment.WebRootPath, "pages", "swagger.html");
        return new FileInfo(path).OpenRead();
    };
});

app.InitModule<CaloBlogHostModule>();

app.Run();
