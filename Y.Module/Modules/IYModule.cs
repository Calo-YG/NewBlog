using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Modules
{
     public interface IYModule
    {
         void ConfigerService(ConfigerServiceContext context);

        void InitApplication(InitApplicationContext context);
    }
}
