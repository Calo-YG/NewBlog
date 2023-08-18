using Y.Module.Modules;

namespace Y.Module
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependOnAttribute : Attribute
    {
        private Type[] _Types;

        public Type[] Types { get { return _Types.Where(p => p.BaseType.Equals(typeof(YModule))).ToArray(); } }

        public DependOnAttribute(params Type[] types)
        {
            _Types = types;
        }
    }
}
