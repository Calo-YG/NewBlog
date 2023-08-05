using Y.Blog.Host;
using Y.Module.Extensions;
using Microsoft.AspNetCore.ResponseCompression;
using Y.Blog.Host.Hubs;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddApplication<BlogHostModule>();

var app = builder.Build();


app.InitApplication();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseResponseCompression();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapHub<QrCodeCheckHubs>("/QrCodehub");

app.MapBlazorHub();

app.MapFallbackToPage("/_Host");



app.Run();
