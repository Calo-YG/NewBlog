using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Middlewares
{
    public static class SwaggerMiddleWare
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app, SwaggerUIOptions options)
        {
            return app.UseMiddleware<UseWithShowSwaggerUI>(options);
        }

        /// <summary>
        /// Register the SwaggerUI middleware with optional setup action for DI-injected options
        /// </summary>
        public static IApplicationBuilder UseWithLoginSwaggerUI(
            this IApplicationBuilder app,
            Action<SwaggerUIOptions> setupAction = null)
        {
            SwaggerUIOptions options;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<SwaggerUIOptions>>().Value;
                setupAction?.Invoke(options);
            }

            // To simplify the common case, use a default that will work with the SwaggerMiddleware defaults
            if (options.ConfigObject.Urls == null)
            {
                var hostingEnv = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                options.ConfigObject.Urls = new[] { new UrlDescriptor { Name = $"{hostingEnv.ApplicationName} v1", Url = "v1/swagger.json" } };
            }

            return app.UseSwaggerUI(options);
        }
    }
}
