namespace Y.Module.Interfaces
{
    public interface IModuleRunner
    {
        void InitApplication(IServiceProvider serviceProvider);

        Task InitApplicationAsync(IServiceProvider serviceProvider);
    }
}
