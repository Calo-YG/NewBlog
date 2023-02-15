using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Modules
{
    public class StartModule
    {
        public Type StartModuleType { get; set; }

        public IYModule Incetance { get; set; }

        public StartModule(Type startModuleType, IYModule incetance)
        {
            StartModuleType = startModuleType;
            Incetance = incetance;
        }
    }
}
