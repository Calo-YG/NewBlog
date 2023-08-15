using Calo.Blog.Host;
using Microsoft.AspNetCore.Builder;
using Y.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ע�����
builder.Services.AddApplication<CaloBlogHostModule>();

var app = builder.Build();
// �����м���ܵ�

await app.InitApplicationAsync();

app.Run();
