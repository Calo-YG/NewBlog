using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Calo.Blog.Common.Y.EventBus
{
    public  class EventHandle<TEvent> : IEventHandle<TEvent>
    {

        private readonly ILogger _logger;

        public EventHandle(ILoggerFactory loggerFactory)
        {
            _logger= loggerFactory.CreateLogger<IEventHandle<TEvent>>();
        }
        public virtual Task HandleAsync(TEvent t)
        {
            throw new NotImplementedException();
        }

        public virtual void OnCompleted()
        {
            _logger.LogInformation("EventBus" + typeof(TEvent).Name + "已完成");
        }
        public virtual void OnError(Exception error)
        {
            _logger.LogInformation("EventBus" + typeof(TEvent).Name + "失败");
        }

        public void OnNext(TEvent value)
        {
            throw new NotImplementedException();
        }
    }
}
