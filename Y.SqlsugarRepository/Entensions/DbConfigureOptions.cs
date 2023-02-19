using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.Entensions
{
    public class DbConfigureOptions
    {
        /// <summary>
        /// 是否启用aop打印sql
        /// </summary>
        public bool EnableAopLog { get; set; }
        /// <summary>
        /// 是否启用打印错误信息
        /// </summary>
        public bool EnableAopError { get; set; }
    }
}
