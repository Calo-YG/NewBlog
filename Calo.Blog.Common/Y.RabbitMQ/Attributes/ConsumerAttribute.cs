namespace Calo.Blog.Common.Y.RabbitMQ.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ConsumerAttribute:Attribute
    {
        public int PreFetchCount { get; private set; } = 1;

        public ConsumerAttribute() { }

        public ConsumerAttribute(int preFetchCount) {
            PreFetchCount = preFetchCount; 
        }   
    }
}
