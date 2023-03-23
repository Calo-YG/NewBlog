using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public interface IEntityManager
    {
        /// <summary>
        /// CodeFirst 创建数据库
        /// </summary>
        void BuildDataBase();
        /// <summary>
        /// 添加种子数据
        /// </summary>
        void DbSeed();
        /// <summary>
        /// 添加种子数据
        /// </summary>
        void DbSeed(Action<ISqlSugarClient> action);
    }
}
