using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.EntityAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute:Attribute
    {
        public bool Enabled { get; }

        private bool _enabled = false;

        public PrimaryKeyAttribute() { }    

        public PrimaryKeyAttribute(bool enabled)
        { 
            _enabled= enabled;
        }
    }
}
