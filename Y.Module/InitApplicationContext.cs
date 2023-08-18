namespace Y.Module
{
    public class InitApplicationContext
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public InitApplicationContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
