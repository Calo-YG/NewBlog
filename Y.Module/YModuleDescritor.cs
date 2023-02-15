using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Interfaces;
using Y.Module.Modules;

namespace Y.Module
{
    public class YModuleDescritor : IYModuleDescritor
    {
        public Type ModuleType { get; }

        public IYModule Incetance { get; }

        public IReadOnlyList<IYModuleDescritor> Descritors { get => _Descritors.ToImmutableArray(); }

        List<IYModuleDescritor> _Descritors { get; set; }

        public YModuleDescritor(Type moduleType, IYModule module)
        {
            ModuleType = moduleType;
            Incetance = module;
        }
        public YModuleDescritor() { }
        public void SetMoudleDescritor(List<IYModuleDescritor> descritors)
        {
            _Descritors = descritors;
        }
    }
}
