using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    public static class YModuleExtenstion
    {
        public static IConfiguration Configuration(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IConfiguration>();
        }
    }
}
