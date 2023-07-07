using RabbitMQ.Client;

namespace Calo.Blog.Common.Y.EventBus.Y.RabbitMQ.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RabbitAtrribute : Attribute
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public virtual string QueueName { get; set; }
        /// <summary>
        /// 路由键
        /// </summary>
        public virtual string? RouteKey { get; set; }
        /// <summary>
        /// 交换机类型--Direct、Fanout
        /// </summary>

        public virtual int Consumer { get; set; } = 1;

        public RabbitAtrribute(string queueName, string routeKey)
        {
            QueueName = queueName;
            RouteKey = routeKey;
        }

        public RabbitAtrribute(string queueName)
        {
            QueueName = queueName;
        }

        public RabbitAtrribute(string queueName, int count)
        {
            QueueName = queueName;
            Consumer = count;
        }

        public RabbitAtrribute(string queueName, string routeKey, int count)
        {
            QueueName = queueName;
            RouteKey = routeKey;
            Consumer = count;
        }

    }
}
