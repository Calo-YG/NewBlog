using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Y.SqlsugarRepository.Repository
{
    public class DbAopProvider : IDbAopProvider
    {
        private readonly ILogger logger;
        public DbAopProvider(ILoggerFactory factory)
        {
            logger=factory.CreateLogger<IDbAopProvider>();
        }

        public virtual Action<string, SugarParameter[]> AopBeforeExecutedTime(ISqlSugarClient db)
        {
            return (sql, param) =>
            {
               
            };
        }

        public virtual Action<SqlSugarException> AopErrorAction()
        {
            return (ex) =>
            {
                logger.LogError(ex.Message, ex.Sql);
            };
        }

        public virtual Action<string, SugarParameter[]> AopAfterExecutedTime (ISqlSugarClient db)
        {
            return (sql, param) =>
            {
                logger.LogInformation(sql, param.Select(p => p.Value));
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
