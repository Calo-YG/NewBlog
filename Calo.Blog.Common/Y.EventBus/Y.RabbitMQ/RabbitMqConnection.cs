using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Calo.Blog.Common.Y.EventBus.Y.RabbitMQ
{
    public class RabbitMqConnection : IRabbitMqConnection , IDisposable
    {
        private readonly ILogger _logger;

        private readonly IConnectionFactory _connectionFactory;
        private bool IsDispod { get; set; }
        public bool Disposed => IsDispod;

        private IConnection _connection;

        private int RetoryCount = 3;

        public RabbitMqConnection(IConnectionFactory connectionFactory
            ,ILoggerFactory loggerFactory) 
        { 
           _connectionFactory= connectionFactory;
           _logger = loggerFactory.CreateLogger<RabbitMqConnection>();  
        }

        public bool IsConnected => _connection is { IsOpen :true} &&  !IsDispod;

        public IModel CreateModel()
        {
            if (IsDispod) throw new ArgumentNullException("RabbitMq 连接已经被释放");
            if (!IsConnected)
            {
                TryConnect();
            }
            _logger.LogInformation("创建MQ通信管道");
            
            return _connection.CreateModel();   
        }

        public void TryConnect()
        {
            if (IsDispod) return;
            try
            {
               _connection= _connectionFactory.CreateConnection();
               _connection.ConnectionShutdown += OnconnectionShoutDown;
               _connection.ConnectionBlocked += OnconnectionBlocked;
               _connection.CallbackException += OnConnectCallBackException;
            }
            catch (Exception)
            {
                RetoryCount--;
                if(RetoryCount <= 0) {
                    _logger.LogWarning("重试连接次数已达最大");
                    throw;
                }
                _logger.LogWarning("重试连接次数" );
                TryConnect();
            }
        }

        private void OnconnectionShoutDown(object? sender, ShutdownEventArgs e)
        {
            if (IsDispod) return;

            TryConnect();
        }

        public void OnConnectCallBackException(object? sender, CallbackExceptionEventArgs e)
        {

            if (IsDispod) return;

            TryConnect();
        }

        private void OnconnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            if (IsDispod) return;

            TryConnect();
        }

        public void Dispose()
        {
            if (IsDispod) return;

            IsDispod = true;

            try
            {
                _connection.ConnectionShutdown -= OnconnectionShoutDown;
                _connection.ConnectionBlocked -= OnconnectionBlocked;
                _connection.CallbackException -= OnConnectCallBackException;
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
