using FreeInterface;
using Microsoft.AspNetCore.ResponseCompression;
using Y.Blog.Host.Data;
using Y.Module;
using Y.Module.Modules;

namespace Y.Blog.Host
{
    [DependOn(typeof(FreeInterfaceModule))]
    public class BlogHostModule:YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            context.Services.AddRazorPages();
            context.Services.AddAntDesign();
            context.Services.AddServerSideBlazor(); 
            context.Services.AddSingleton<WeatherForecastService>();
            context.Services.AddScoped<LocalPhotoService>();

            context.Services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                      new[] { "application/octet-stream","application/json" });
            });
        }
    }
}
