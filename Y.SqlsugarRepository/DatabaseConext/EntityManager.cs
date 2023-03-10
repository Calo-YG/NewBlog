using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public class EntityManager : IEntityManager
    {
        private readonly IEntityContainer _entityContainer;
        private readonly ISqlSugarClient _client;
        private readonly ILogger<IEntityManager> _logger;
        public EntityManager(IEntityContainer entityContainer
            , ISqlSugarClient client
            , ILogger<IEntityManager> logger)
        {
            _entityContainer = entityContainer;
            _client = client;
            _logger = logger;
        }
        /// <summary>
        /// CodeFirst 创建数据库
        /// </summary>
        public virtual void BuildDataBase()
        {

        }
        /// <summary>
        /// 添加种子数据
        /// </summary>
        public virtual void DbSeed() { }
        /// <summary>
        /// 添加种子数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual void DbSeed<T>()
        {

        }
    }
}
