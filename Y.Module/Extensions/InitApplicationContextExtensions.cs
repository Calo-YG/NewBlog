using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Y.Module.Interfaces;

namespace Y.Module.Extensions
{
    public static class InitApplicationContextExtensions
    {
        public static void CheckNull(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException("IAppBuilder为空");
        }

        public static IApplicationBuilder GetApplicationBuilder(this InitApplicationContext context)
        {
            return context.ServiceProvider.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value;
        }

        public static void InitApplication(this IApplicationBuilder app)
        {
            InitBaseSetting(app);

            var runner = app.ApplicationServices.GetRequiredService<IModuleRunner>();
            runner.InitApplication(app.ApplicationServices);
        }

        public static async Task InitApplicationAsync(this IApplicationBuilder app)
        {
            InitBaseSetting(app);
            var runner = app.ApplicationServices.GetRequiredService<IModuleRunner>();
            await runner.InitApplicationAsync(app.ApplicationServices);
        }

        /// <summary>
        /// 初始化基础
        /// </summary>
        /// <param name="app"></param>
        private static void InitBaseSetting(IApplicationBuilder app)
        {
            app.CheckNull();

            InitApplicationContext context = new InitApplicationContext(app.ApplicationServices);

            app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
            app.ApplicationServices.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value = app;

            app.ApplicationServices.GetRequiredService<ObjectAccessor<InitApplicationContext>>().Value = context;
            app.ApplicationServices.GetRequiredService<IObjectAccessor<InitApplicationContext>>().Value = context;
        }
    }
}
