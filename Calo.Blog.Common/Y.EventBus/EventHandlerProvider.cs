using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Y.Module.Extensions;

namespace Calo.Blog.Common.Y.EventBus
{
    public class EventHandlerProvider : IEventHandlerProvider
    {
        private readonly IServiceCollection _service;
        public EventHandlerProvider(IServiceCollection service)
        {
            _service = service ?? throw new ArgumentNullException("EventBust中EventHandlerProvider为空");
        }
        public virtual void AddEventHandle<TEvent,THandle>()
        {
           var exists = _service.IsExists<IEventHandle<TEvent>>();
           Check<THandle>();
           if(!exists)
           {
                _service.AddTransient(typeof(IEventHandle<TEvent>),typeof(THandle));
           }
        }

        private void Check<THandle>()
        {
            var type = typeof(THandle);
            var isEntity = typeof(THandle).GetTypeInfo()
                              .GetInterfaces()
                              .Any(p => p.GetTypeInfo().IsGenericType && p.GetGenericTypeDefinition() == typeof(IEventHandle<>));
            if (!isEntity)
            {
                throw new ApplicationException("EventHandle: " + type.Name + ". 确认实体是否继承了IEventHandle接口");
            }
        }
    }
}
