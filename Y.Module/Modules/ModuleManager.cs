using Microsoft.Extensions.Logging;
using Y.Module.Interfaces;

namespace Y.Module.Modules
{
    public class ModuleManager : IModuleManager
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly IModuleContainer _moduleContainer;

        private readonly ILogger<IModuleManager> _logger;

        private readonly IObjectAccessor<InitApplicationContext> _initApplicationAccessor;
        public ModuleManager(IModuleContainer moduleContainer
            , IServiceProvider serviceProvider
            , ILogger<IModuleManager> logger
            , IObjectAccessor<InitApplicationContext> initApplicationAccessor)
        {
            _serviceProvider = serviceProvider;
            _moduleContainer = moduleContainer;
            _logger = logger;
            _initApplicationAccessor = initApplicationAccessor;
        }

        public void IninAppliaction()
        {
            InitApplicationContext? context = _initApplicationAccessor.Value;
            foreach (var module in _moduleContainer.Modules)
            {
                var name = module.Incetance.GetType().Name;
                _logger.LogInformation($"模块:{name}开始初始化");

                module.Incetance.InitApplication(context);

                if(module.Incetance is ILaterApplication application)
                {
                    application.LaterInitApplication(context);
                }

                _logger.LogInformation($"模块:{name}初始化完成");
            }
        }

        public async Task InitApplicationAsync()
        {
            InitApplicationContext? context = _initApplicationAccessor.Value;
            foreach (var module in _moduleContainer.Modules)
            {
                var name = module.Incetance.GetType().Name;
                _logger.LogInformation($"模块:{name}开始初始化");

                module.Incetance.InitApplication(context);
                //异步初始化
                if (module.Incetance is ILaterApplicationAsync applicationAsync)
                {
                    await applicationAsync.LaterInitApplicationAsync(context);
                }
                if (module.Incetance is ILaterApplication application)
                {
                    application.LaterInitApplication(context);
                }

                _logger.LogInformation($"模块:{name}初始化完成");
            }

        }
    }
}
