using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.EntityAttribute
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    public class KeyWithIncrementAttribute:Attribute
    {
        public bool IsIncrement { get=>_isIncrement;}

        private bool _isIncrement = false;

        public KeyWithIncrementAttribute() { }
        public KeyWithIncrementAttribute(bool isIncrement)
        {
            _isIncrement = isIncrement;
        }
    }
}
