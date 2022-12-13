using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.EntityBase
{
    public class AutiedEntity<T> :Entity<T>
    {
        /// <summary>
        /// 创建人id
        /// </summary>
        public T? UpdaterUserId { get; set; }

        public T? UpdateUserId { get; set; }
    }
}
