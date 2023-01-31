using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependOnAttribute:Attribute
    {
        private Type[] _type;

        public Type[] Type { get => _type.Where(p => p.BaseType == typeof(string)).ToArray(); }
        public DependOnAttribute(params Type[] types)
        {
            _type = types;
        }
    }
}
