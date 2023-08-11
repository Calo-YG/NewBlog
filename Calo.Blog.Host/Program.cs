using Calo.Blog.Application.Interfaces;
using Calo.Blog.Application.SourceTypeManager;
using Calo.Blog.Host;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Y.Module.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication<CaloBlogHostModule>();

var app = builder.Build();
// Configure the HTTP request pipeline.

await app.InitApplicationAsync();

app.Run();
