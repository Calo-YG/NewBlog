using System.Diagnostics.CodeAnalysis;

namespace Calo.Blog.Common.Y.RabbitMQ
{
    public class QueueConfig
    {
        public string QueueName { get; private set; }
        /// <summary>
        /// 持久化队列保证消息不丢失
        /// </summary>
        public bool Durable { get; private set; } = true;
        /// <summary>
        /// 设置排他--进当前连接可用
        /// </summary>
        public bool Exclusive { get; private set; } = false;
        /// <summary>
        /// 自动删除队列
        /// </summary>
        public bool AutoDelete { get; private set; } = true;
        /// <summary>
        /// 队列参数
        /// </summary>
        public Dictionary<string,object> Arguments { get; private set; }

        public QueueConfig([NotNull]string queueName)
        {
            QueueName = queueName;
            Arguments = new Dictionary<string, object>();
        }

        public QueueConfig(string queueName, 
            [NotNull]Dictionary<string, object> arguments)
        {
            QueueName = queueName;
            Arguments = arguments ?? new Dictionary<string, object>();
        }

        public QueueConfig([NotNull]string queueName, 
            [NotNull]bool durable, 
            [NotNull]bool exclusive,
            [NotNull] bool autoDelete,
            [NotNull]Dictionary<string, object> arguments)
        {
            QueueName = queueName;
            Durable = durable;
            Exclusive = exclusive;
            AutoDelete = autoDelete;
            Arguments = arguments;
        }
    }
}
