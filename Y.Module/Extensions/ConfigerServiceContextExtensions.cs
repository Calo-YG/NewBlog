using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Extensions
{
    public static class ConfigerServiceContextExtensions
    {
        public static IConfiguration GetConfiguartion(this ConfigerServiceContext context)
        {
            if (context is null || context.Services is null) throw new ArgumentException("context is null");
            return context.Provider.GetRequiredService<IConfiguration>();
        }

        public static IHostingEnvironment Environment(this ConfigerServiceContext context)
        {
            if (context is null || context.Services is null) throw new ArgumentException("context is null");
            return context.Provider.GetRequiredService<IHostingEnvironment>();
        }
    }
}
