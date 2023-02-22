﻿using Microsoft.AspNetCore.Builder;
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

        public static IApplicationBuilder GetApplicationBuilder(this InitApplicationContext context)
        {
            return context.ServiceProvider.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value;
        }

        public static void InitApplication(this IApplicationBuilder app)
        {
            app.CheckNull();

            InitApplicationContext context = new InitApplicationContext(app.ApplicationServices);

            app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
            app.ApplicationServices.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value = app;

            app.ApplicationServices.GetRequiredService<ObjectAccessor<InitApplicationContext>>().Value = context;
            app.ApplicationServices.GetRequiredService<IObjectAccessor<InitApplicationContext>>().Value = context;

            var runner = app.ApplicationServices.GetRequiredService<IModuleRunner>();
            runner.InitApplication(app.ApplicationServices);
        }
    }
}
