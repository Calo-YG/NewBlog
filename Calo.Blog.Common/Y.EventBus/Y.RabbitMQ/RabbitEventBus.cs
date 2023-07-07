namespace Calo.Blog.Common.Y.EventBus.Y.RabbitMQ
{
    public class RabbitEventBus : IRabbitEventBus
    {

        public RabbitEventBus() { }
        public void Publish<TEvent>(TEvent @event)
        {
            
        }

        public Task PublishAsync(object @event)
        {
            return Task.CompletedTask;
        }
    }
}
