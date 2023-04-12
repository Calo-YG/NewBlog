namespace Calo.Blog.Common.Y.EventBus
{
    public interface IEventHandle<TEvent>:IObserver<TEvent>
    {
        Task HandleAsync(TEvent t);
    }
}
