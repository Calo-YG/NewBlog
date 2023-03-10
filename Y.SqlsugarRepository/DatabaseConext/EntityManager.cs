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
        private IReadOnlyList<Type> EntityTypes { get; set; }
        public EntityManager(IEntityContainer entityContainer
            , ISqlSugarClient client
            , ILogger<IEntityManager> logger)
        {
            _entityContainer = entityContainer;
            _client = client;
            _logger = logger;
            EntityTypes = _entityContainer.EntityTypes;
        }
        /// <summary>
        /// CodeFirst 创建数据库
        /// </summary>
        public virtual void BuildDataBase()
        {
            _logger.LogInformation("数据库建表开始");
            try
            {
                foreach (var item in EntityTypes)
                {
                    _logger.LogInformation($"{item.Name}开始创建");
                    _client.CodeFirst
                        .SetStringDefaultLength(200)
                        .InitTables(item);
                    _logger.LogInformation($"{item.Name}创建完成");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("数据库建表异常" + ex.ToString());
                throw;
            }
            finally
            {
                _logger.LogError("数据库建表完成");
            }
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
