using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Interfaces;

namespace Y.Module.Extensions
{
    public static class InitApplicationContextExtensions
    {
        public static void CheckNull(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException("IAppBuilder为空");
        }

        public static void InitApplication(this IApplicationBuilder app)
        {
            app.CheckNull();
            app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value= app;
            var runner = app.ApplicationServices.GetRequiredService<IModuleRunner>();
            runner.InitApplication(app.ApplicationServices);
        }
    }
}
