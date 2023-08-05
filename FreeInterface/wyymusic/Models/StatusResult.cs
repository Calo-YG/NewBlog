namespace FreeInterface.wyymusic.Models
{
    public class StatusResult
    {
        public int Code { get; set; }   

        public WyyAccount Account { get; set; }

        public WyyProfile Profile { get; set; } 
    }
}
