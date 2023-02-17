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
        public void ConfigerService(ConfigerServiceContext context);

        public void InitApplication(InitApplicationContext context);
    }
}
