using Y.Module.Interfaces;
using Y.Module.Modules;

namespace Y.Module
{
    public class YModuleDescritor : IYModuleDescritor
    {
        public Type ModuleType { get; }

        public IYModule Incetance { get; }

        public YModuleDescritor(Type moduleType, IYModule module)
        {
            ModuleType = moduleType;
            Incetance = module;
        }
        public YModuleDescritor() { }
    }
}
