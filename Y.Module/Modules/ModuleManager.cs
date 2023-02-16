using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Interfaces;

namespace Y.Module.Modules
{
    public class ModuleManager : IModuleManager
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IModuleContainer _moduleContainer;

        private readonly ILogger<IModuleManager> _logger;
        public ModuleManager(IModuleContainer moduleContainer
            ,IServiceProvider serviceProvider
            ,ILogger<IModuleManager> logger) 
        { 
             _serviceProvider= serviceProvider;
            _moduleContainer= moduleContainer;
            _logger= logger;
        }

        public void IninAppliaction()
        {
            InitApplicationContext context = new InitApplicationContext(_serviceProvider);
            foreach(var module in _moduleContainer.Modules)
            {
                var name = module.Incetance.GetType().Name;
                _logger.LogInformation($"模块:{name}开始初始化");
                module.Incetance.InitApplication(context);
                _logger.LogInformation($"模块:{name}初始化完成");
            }
        }
    }
}
