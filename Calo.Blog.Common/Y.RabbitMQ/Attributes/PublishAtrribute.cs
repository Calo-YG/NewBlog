namespace Calo.Blog.Common.Y.RabbitMQ.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PublishAtrribute:Attribute
    {
        public virtual string QueueName { get; set; }
        public virtual bool? UseRouteKey { get; set; } = false;
        public virtual string? RouteKey { get; set; }

        public PublishAtrribute(string queueName) 
        {
           QueueName= queueName;
        }
    }
}
