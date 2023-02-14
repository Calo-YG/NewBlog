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
        public Type TypeType { get; }

        public IYModule Intance { get; }

        public IReadOnlyList<IYModuleDescritor> Modules { get; }
    }
}
