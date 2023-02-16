using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Interfaces;

namespace Y.Module
{
    public class ModuleApplication : ModuleApplicationBase , IModuleRunner
    {
        public ModuleApplication(Type startModuleType, IServiceCollection services) : base(startModuleType, services)
        {
            services.AddSingleton<IModuleRunner>(this);
        }

        public override void InitApplication(IServiceProvider serviceProvider)
        {
            SetServiceProvider(serviceProvider);
            base.InitApplication(serviceProvider);
        }
    }
}
