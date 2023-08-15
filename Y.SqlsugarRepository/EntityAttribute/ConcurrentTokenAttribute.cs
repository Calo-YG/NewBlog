namespace Y.SqlsugarRepository.EntityAttribute
{
    //sqlsugar 支持并发冲突（类似于悲观锁）
    [AttributeUsage(AttributeTargets.Property)]
    public class ConcurrentTokenAttribute:Attribute
    {
        public bool Enabled { get=>_enabled; }

        private bool _enabled=true;

        public ConcurrentTokenAttribute() { }

        public ConcurrentTokenAttribute(bool enabled)
        {
            _enabled = enabled;
        }   
    }
}
