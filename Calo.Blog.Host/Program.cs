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
