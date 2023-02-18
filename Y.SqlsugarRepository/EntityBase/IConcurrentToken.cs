using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.EntityBase
{
    /// <summary>
    /// 并发token
    /// </summary>
    public interface IConcurrentToken
    {
        public string? ConcurrentToken { get; set; }
    }
}
