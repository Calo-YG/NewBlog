using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.DependencyInjection.AutoFacDependencyInjection
{
    public interface IIocManager:IDisposable
    {
        bool IsRegistered<TType>() where TType : notnull;

        bool IsRegistered(Type type);
    }
}
