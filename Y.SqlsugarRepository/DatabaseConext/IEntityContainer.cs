using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public interface IEntityContainer
    {
        /// <summary>
        /// 数据库实体类型集合
        /// </summary>
        IReadOnlyList<Type> EntityTypes { get; set; }
    }
}
