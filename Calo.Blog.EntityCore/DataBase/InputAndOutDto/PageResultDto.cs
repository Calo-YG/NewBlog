using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.InputAndOutDto
{
    public class PageResultDto<TEntity>
    {
        public RefAsync<int> TotalCount { get; set; }

        public List<TEntity> Items { get; set; }
    }
}
