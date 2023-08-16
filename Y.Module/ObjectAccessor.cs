using Y.Module.Interfaces;

namespace Y.Module
{
    public class ObjectAccessor<T> : IObjectAccessor<T>
    {
        public T? Value { get;  set; }
        public ObjectAccessor() { }

        public ObjectAccessor(T value) { Value = value; }
    }
}
