using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.InputEntity
{
    public class PageInput
    {
        /// <summary>
        /// 分页数量
        /// </summary>
        public int MaxCount { get; set; }
        /// <summary>
        /// 从第多页也开始
        /// </summary>
        public int SkipCount { get; set; }
    }
}
