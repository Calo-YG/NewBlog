namespace FreeInterface.wyymusic.Models
{
    public class WyyResponse<TResult>
    {
        public TResult Data { get; set; }

        public string Code { get; set; }    
    }
}
