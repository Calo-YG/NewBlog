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

// ע�����
builder.Services.AddApplication<CaloBlogHostModule>();


var app = builder.Build();

// �����м���ܵ�

await app.InitApplicationAsync();

app.Run();
