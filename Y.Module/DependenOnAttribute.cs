using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Modules;

namespace Y.Module
{

    public class DependenOnAttribute : Attribute
    {
        private Type[] _Types;

        public Type[] Types { get { return _Types.Where(p => p.BaseType.Equals(typeof(YModule))).ToArray(); } }

        public DependenOnAttribute(params Type[] types)
        {
            _Types = types;
        }
    }
}
