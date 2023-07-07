using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using ZstdSharp.Unsafe;

namespace Calo.Blog.Common.Y.EventBus.Y.RabbitMQ
{
    public class RabbitMqSubcriptionManager : IRabbitMqSubscriptionManager
    {
        public ConcurrentDictionary<Type, List<EventHandlerDiscriptor>> EventSubcriptions => _eventSubcriptions;

        ConcurrentDictionary<Type, List<EventHandlerDiscriptor>> _eventSubcriptions = new ConcurrentDictionary<Type, List<EventHandlerDiscriptor>>();


        private readonly IRabbitMqConnection _connection;
        private readonly ILogger _logger;

        private IModel Channel;
        
        public RabbitMqSubcriptionManager(IRabbitMqConnection connection,ILoggerFactory loggerFactory)
        {
            _connection = connection;
            _logger = loggerFactory.CreateLogger<RabbitMqSubcriptionManager>();
            InitChannel();
        }

        /// <summary>
        /// 初始化通信管道
        /// </summary>
        private void InitChannel()
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }
            _logger.LogInformation("尝试创建RabbitMq 通信信道");
            Channel = _connection.CreateModel();
        }

        public virtual void Onsubcription<T, Th>()
        {
            
        }

        public virtual void Unsubcription<T, Th>()
        {
            throw new NotImplementedException();
        }


        
    }
}
