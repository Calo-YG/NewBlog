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
        public TPrimarykey UpdaterUserId { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}
