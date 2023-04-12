namespace Calo.Blog.Common.Y.EventBus
{
    public interface IEventHandlerProvider
    {
        void AddEventHandle<TEvent, THandle>();
    }
}
