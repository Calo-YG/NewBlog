using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Interfaces
{
    public interface IModuleApplication
    {
        Type StartModuleType { get; }
        IServiceCollection Services { get; }    

        IServiceProvider ServiceProvider { get; }

        void ConfigerService();

        void InitApplication();
    }
}