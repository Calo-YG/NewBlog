using Autofac;
using Autofac.Extensions.DependencyInjection;
using Calo.Blog.EntityCore;
using Calo.Blog.EntityCore.DataBase;
using Calo.Blog.EntityCore.DataBase.Extensions;
using Calo.Blog.EntityCore.DataBase.Repository;
using Calo.Blog.Extenions.DependencyInjection.AutoFacDependencyInjection;
using Calo.Blog.Host.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ResultFilter>();
});

builder.Services.AddSqlSugarClientAsCleint(p =>
{
    p.ConnectionString = builder.Configuration.GetSection("").Value;
    p.DbType = SqlSugar.DbType.SqlServer;
    p.IsAutoCloseConnection = true;
}).AddSuagarDbContextAsScoped<BlogContext>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Calo API",
        Description = "Web API for managing By Calo",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule<EntityCoreModuleRegister>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
