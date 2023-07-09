using FreeInterface;
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
            //base.ConfigerService(context);
            context.Services.AddRazorPages();
            context.Services.AddAntDesign();
            context.Services.AddServerSideBlazor(); 
            context.Services.AddSingleton<WeatherForecastService>();
            context.Services.AddScoped<LocalPhotoService>();
        }
    }
}
