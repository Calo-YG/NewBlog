using Microsoft.Extensions.DependencyInjection;

namespace Calo.Blog.Common.Y.EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        
        public EventBus(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async void Publish<TEvent>(TEvent @event)
        {
           using var scope = _serviceScopeFactory.CreateScope();

           var handle = scope.ServiceProvider.GetRequiredService<IEventHandle<TEvent>>();
            try
            {
                await handle.HandleAsync(@event);
                handle.OnCompleted();
            }
            catch (Exception ex)
            {
                handle.OnError(ex);
                throw;
            }
           
        }
    }
} 
