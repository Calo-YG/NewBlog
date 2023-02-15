using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Modules;

namespace Y.Module.Interfaces
{
    public interface IYModuleDescritor
    {
        public Type ModuleType { get; }

        public IYModule Incetance { get; }

        public IReadOnlyList<IYModuleDescritor> Descritors { get; }
    }
}
