using Y.EventBus;

namespace Calo.Blog.Application.ResourceOwnereServices.Etos
{
    [EventDiscriptor("test",1000,false)]
    public class TestEto
    {
        public string Name { get; set; }    

        public string Description { get; set; } 
    }
}
