﻿namespace Y.SqlsugarRepository.EntityAttribute
{
    //sqlsugar 支持并发冲突（乐观锁）
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
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
