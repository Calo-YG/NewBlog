using Calo.Blog.Common.Y.EventBus.Y.RabbitMQ;
using System.Collections.Concurrent;

namespace Calo.Blog.Common.Y.EventBus
{
    public interface IEventSubcriptionManager
    {
        public ConcurrentDictionary<Type,List<EventHandlerDiscriptor>> EventSubcriptions { get;}

        public void Onsubcription<T,Th>();

        public void Unsubcription<T, Th>();
    }
}
