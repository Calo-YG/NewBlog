using Autofac;
using Autofac.Extensions.DependencyInjection;
using Calo.Blog.Host;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Y.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
.ConfigureContainer<ContainerBuilder>(container =>
{
    // container.RegisterModule<EntityCoreModuleRegister>();
});
builder.Services.AddApplication<CaloBlogHostModule>();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.InitApplication();

app.Run();
