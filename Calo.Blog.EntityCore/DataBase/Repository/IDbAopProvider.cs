using Calo.Blog.EntityCore.DataBase.Extensions;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Repository
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
