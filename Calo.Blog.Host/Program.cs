using Calo.Blog.Host;
using Microsoft.AspNetCore.Builder;
using Y.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 注入服务
builder.Services.AddApplication<CaloBlogHostModule>();

var app = builder.Build();
// 创建中间件管道

await app.InitApplicationAsync();

app.Run();
