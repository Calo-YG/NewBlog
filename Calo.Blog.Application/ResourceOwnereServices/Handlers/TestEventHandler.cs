using Calo.Blog.Application.ResourceOwnereServices.Etos;
using Microsoft.Extensions.Logging;
using Y.EventBus;
using Y.Module.DependencyInjection;

namespace Calo.Blog.Application.ResourceOwnereServices.Handlers
{
    public class TestEventHandler : IEventHandler<TestEto>,ITransientDependency
    {
        private ILogger _logger;
        public TestEventHandler(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger<TestEventHandler>();
        }   
        public Task HandelrAsync(TestEto eto)
        {
            _logger.LogInformation($"{typeof(TestEto).Name}--{eto.Name}--{eto.Description}");
            return Task.CompletedTask;
        }
    }
}
