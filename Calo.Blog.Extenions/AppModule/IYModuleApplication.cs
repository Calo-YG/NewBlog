using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    internal interface IModuleApplication
    {
        Type StartModuleType { get; }
        IServiceCollection Services { get; }

        IServiceProvider Provider { get; }

        IReadOnlyList<IYModule> Modules { get; }

        IEnumerable<IYModule> Source { get; }
    }
}
