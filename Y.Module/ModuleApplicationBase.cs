using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Interfaces;

namespace Y.Module
{
    public class ModuleApplicationBase : IModuleApplication
    {
        public Type StartModuleType => throw new NotImplementedException();

        public IServiceCollection Services => throw new NotImplementedException();

        public IServiceProvider ServiceProvider => throw new NotImplementedException();

        public void ConfigerService()
        {
            throw new NotImplementedException();
        }

        public void InitApplication()
        {
            throw new NotImplementedException();
        }
    }
}
