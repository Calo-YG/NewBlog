using Autofac;
using Autofac.Extensions.DependencyInjection;
using Calo.Blog.EntityCore;
using Calo.Blog.EntityCore.DataBase;
using Calo.Blog.EntityCore.DataBase.Extensions;
using Calo.Blog.EntityCore.DataBase.Repository;
using Calo.Blog.Extenions.DependencyInjection.AutoFacDependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
        Version = "1.0",
        Title = "CaloApi",
        Description = "CalaAPI"
    });
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

app.UseSwaggerUI(p =>
{

});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
