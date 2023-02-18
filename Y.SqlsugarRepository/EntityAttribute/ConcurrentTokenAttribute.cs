using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.EntityAttribute
{
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
