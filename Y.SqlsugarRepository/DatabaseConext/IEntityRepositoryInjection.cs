using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public interface IEntityRepositoryInjection
    {
        /// <summary>
        /// IServiceCollection
        /// </summary>
        IServiceCollection Services { get; set; }
        /// <summary>
        /// 添加数据库仓储
        /// </summary>
        void AddRepository();

        void LoadEntity(IEntityProvider provider);
    }
}
