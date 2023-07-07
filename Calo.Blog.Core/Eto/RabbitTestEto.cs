using Calo.Blog.Common.Y.EventBus.Y.RabbitMQ.Attributes;

namespace Calo.Blog.Domain.Eto
{
    [RabbitAtrribute("wygrabbitMqTest",2)]
    public class RabbitTestEto
    {
        public string Name { get; set; }

        public string Description { get; set; } 
    }
}
