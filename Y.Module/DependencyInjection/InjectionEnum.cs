using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.DependencyInjection
{
    public enum InjectionEnum
    {
        //单例
        Singleton,
        //作用域
        Scoped,
        //瞬时
        Transient
    }
}
