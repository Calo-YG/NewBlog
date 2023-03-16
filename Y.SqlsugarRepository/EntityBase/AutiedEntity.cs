using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.EntityBase
{
    public class AutiedEntity<TPrimarykey> : Entity<TPrimarykey>
    {
        /// <summary>
        /// 创建人id
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public TPrimarykey? UpdaterUserId { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? UpdateTime { get; set; }
    }
}
