using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    public class ServiceConfigurationContext
    {
        public IServiceCollection Service { get; private set; }
        public IServiceProvider ServiceProvider { get => Service.BuildServiceProvider(); }

        public ServiceConfigurationContext(IServiceCollection service)
        {
            Service = service;
        }
    }
}
