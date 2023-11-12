using Calo.Blog.Host;
using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Y.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (context, services, configuration) =>
        configuration.ReadFrom
            .Configuration(context.Configuration)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            
            .WriteTo.Console());

// 注入服务
builder.Services.AddApplication<CaloBlogHostModule>();


var app = builder.Build();

// 创建中间件管道

await app.InitApplicationAsync();

app.Run();
