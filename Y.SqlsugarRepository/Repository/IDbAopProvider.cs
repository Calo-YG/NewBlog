using SqlSugar;

namespace Y.SqlsugarRepository.Repository
{
    public interface IDbAopProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Action<SqlSugarException> AopErrorAction();

        Action<string, SugarParameter[]> AopAfterExecutedTime(ISqlSugarClient db);

        Action<string, SugarParameter[]> AopBeforeExecutedTime(ISqlSugarClient db);
    }
}
