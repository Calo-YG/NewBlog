namespace Y.Blog.Host.Models
{
    public class WorkItem
    {
        public string Icon { get; set; }    

        public string Title { get; set; }

        public string Description { get; set; }

        public Action CallBack { get; set; }

        public WorkItem() { }

        public WorkItem(string icon, string title, string description,Action action)
        {
            Icon = icon;
            Title = title;
            Description = description;
            CallBack = action;
        }   
    }
}
