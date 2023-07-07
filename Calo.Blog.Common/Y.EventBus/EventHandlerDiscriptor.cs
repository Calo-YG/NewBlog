namespace Calo.Blog.Common.Y.EventBus
{
    /// <summary>
    /// EventHandler描述
    /// </summary>
    public class EventHandlerDiscriptor
    {
        public Type TEvent { get; set; }

        public Type Handler { get; set; }

        public EventHandlerDiscriptor(Type tevent,Type handler) 
        { 
           TEvent= tevent;
           Handler= handler;   
        }
    }
}
