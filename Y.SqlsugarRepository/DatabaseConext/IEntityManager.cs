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
        void BuildContext();
        /// <summary>
        /// 添加种子数据
        /// </summary>
        void DbSeed();
        /// <summary>
        /// 添加种子数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void DbSeed<T>();
    }
}
