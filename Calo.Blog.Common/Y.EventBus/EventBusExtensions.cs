using Microsoft.Extensions.DependencyInjection;
using Y.Module.Extensions;

namespace Calo.Blog.Common.Y.EventBus
{
    public static class EventBusExtensions
    {
       public static void AddEventHandle(this IServiceCollection services,Action<IEventHandlerProvider> action)
        {
            services.ChcekNull();
            IEventHandlerProvider handlerProvider = new EventHandlerProvider(services);
            action.Invoke(handlerProvider);
        }
    }
}
