using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    internal  interface  IObjectAccessor<T>
    {
        internal T? Value { get; set; }
    }
}
