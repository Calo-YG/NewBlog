using Microsoft.Extensions.DependencyInjection;

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

        public ConfigerServiceContext(IServiceCollection services)
        {
            Services = services;
        }
    }
}
