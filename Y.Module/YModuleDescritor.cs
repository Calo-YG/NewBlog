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
        public Type TypeType { get; }

        public IYModule Intance { get; }

        public IReadOnlyList<IYModuleDescritor> Modules { get=>_Modules.ToImmutableArray(); }

        List<IYModuleDescritor> _Modules { get; set; }


    }
}
