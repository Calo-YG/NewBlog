using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.DatabaseConext;

namespace Y.SqlsugarRepository.Repository
{
    public class DbAopProvider:IDbAopProvider
    {
        private readonly IOptions<DbConfigureOptions> _dboptions;
        public DbConfigureOptions DbConfigureOptions { get => _dboptions.Value; }
        public DbAopProvider(IOptions<DbConfigureOptions> dboptions)
        {
            _dboptions = dboptions;
        }

        public virtual Action<string, SugarParameter[]> AopLogAction(ILogger logger)
        {
            return (sql, paras) =>
            {
                logger.LogInformation(sql, paras.Select(p => p.Value));
            };
        }

        public virtual Action<SqlSugarException> AopErrorAction(ILogger logger)
        {
            return (ex) =>
            {
                logger.LogError(ex.Message, ex.Sql);
            };
        }

        /// <summary>
        /// 字段赋值Aop待后续添加身份验证后完善，暂时不对外暴露
        /// </summary>
        /// <returns></returns>
        public virtual Action<object, DataFilterModel> AopSetValue()
        {
            return (obj, model) =>
            {

            };
        }
    }
}
