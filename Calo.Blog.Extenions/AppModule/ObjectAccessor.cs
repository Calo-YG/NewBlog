using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    internal abstract class ObjectAccessor<T> : IObjectAccessor<T>
    {
        T? IObjectAccessor<T>.Value { get; set; }
    }
}
