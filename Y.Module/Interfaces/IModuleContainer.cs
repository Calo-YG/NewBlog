namespace Y.Module.Interfaces
{
    public interface IModuleContainer
    {
        IReadOnlyList<IYModuleDescritor> Modules { get; }
    }
}
