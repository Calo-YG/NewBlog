using Microsoft.Extensions.DependencyInjection;
namespace Y.Module.Interfaces
{
    public interface IModuleManager
    {
        void IninAppliaction();
        Task InitApplicationAsync();
    }
}
