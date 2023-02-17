
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module
{
    public class ConfigerServiceContext 
    {
        public IServiceCollection Services { get; private set; }

        public IServiceProvider Provider { get
            {
                if(Services is null)
                {
                    throw new ArgumentNullException(nameof(Services)+"ConfigerServiceContext中Service为空");
                }
                return Services.BuildServiceProvider();
            }
        }

        public object Service { get; set; }

        public ConfigerServiceContext(IServiceCollection services)
        {
            Services = services;
        }
    }
}
