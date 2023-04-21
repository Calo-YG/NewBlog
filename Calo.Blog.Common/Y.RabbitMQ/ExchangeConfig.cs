using RabbitMQ.Client;

namespace Calo.Blog.Common.Y.RabbitMQ
{
    public class ExchangeConfig
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; private set; }
        /// <summary>
        /// 交换机类型
        /// </summary>
        public string Type { get; private set; } = ExchangeType.Direct;
        /// <summary>
        /// 消息持久化
        /// 默认消息持久化
        /// </summary>
        public bool Durable { get; private set; } = true;
        /// <summary>
        /// 自动删除
        /// 一个或多与该交换机绑定的交换机或者队列解除绑定删除该交换机
        /// </summary>
        public bool AutoDelete { get; private set; } = false;
        /// <summary>
        /// 内置交换器只能通过交换器进行交换器的处理
        /// </summary>
        public bool Internal { get; private set; } = false;
        /// <summary>
        /// 处理参数
        /// </summary>
        public Dictionary<string,object> Arguments { get; private set; }

        public ExchangeConfig(string exchangeName)
        {
            ExchangeName = exchangeName;
            Arguments = new Dictionary<string, object>();
        }

        public ExchangeConfig(string exchangeName,Dictionary<string,object> args)
        {
            ExchangeName = exchangeName;
            Arguments = args ?? new Dictionary<string, object>();
        }

        public ExchangeConfig(string exchangeName, string type, bool durable, bool autoDelete, bool @internal, Dictionary<string, object> arguments)
        {
            ExchangeName = exchangeName;
            Type = type;
            Durable = durable;
            AutoDelete = autoDelete;
            Internal = @internal;
            Arguments = arguments;
        }
    }
}
