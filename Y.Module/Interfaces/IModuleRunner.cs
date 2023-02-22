using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Interfaces
{
    public interface IModuleRunner
    {
        void InitApplication(IServiceProvider serviceProvider);

        void LaterApplication(IServiceProvider serviceProvider);
    }
}
