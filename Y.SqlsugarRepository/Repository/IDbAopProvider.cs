using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.Entensions;

namespace Y.SqlsugarRepository.Repository
{
    public interface IDbAopProvider
    {
        public DbConfigureOptions DbConfigureOptions { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Action<string, SugarParameter[]> AopLogAction(ILogger logger);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Action<SqlSugarException> AopErrorAction(ILogger logger);
    }
}
