using Autofac;
using Autofac.Extensions.DependencyInjection;
using Calo.Blog.Extenions.AppModule;
using Calo.Blog.Host;
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

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
.ConfigureContainer<ContainerBuilder>(container =>
{
    // container.RegisterModule<EntityCoreModuleRegister>();
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
