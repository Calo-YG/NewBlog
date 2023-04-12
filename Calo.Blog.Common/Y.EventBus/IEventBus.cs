namespace Calo.Blog.Common.Y.EventBus
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event);
    }
}
