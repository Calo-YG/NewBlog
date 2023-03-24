using Calo.Blog.Host;
using Microsoft.AspNetCore.Builder;
using Y.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication<CaloBlogHostModule>();

var app = builder.Build();
// Configure the HTTP request pipeline.

app.InitApplication();

app.Run();
