using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<IEntityManager> _logger;
        private readonly IOptions<DatabaseSetting> _options;
        private IReadOnlyList<Type> EntityTypes { get; set; }
        public EntityManager(IEntityContainer entityContainer
            , IServiceProvider serviceProvider
            , ILogger<IEntityManager> logger
            , IOptions<DatabaseSetting> options)
        {
            _entityContainer = entityContainer;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _options = options;
            EntityTypes = _entityContainer.EntityTypes;
        }
        /// <summary>
        /// CodeFirst 创建数据库
        /// </summary>
        public virtual void BuildDataBase()
        {
            //跳过建库建表，加快启动速度
            if (_options.Value.SikpBuildDatabase)
            {
                _logger.LogInformation("已跳过建库建表，如有需要请修改DatabaseSetting配置");
                return;
            }
            using var scope = _serviceProvider.CreateScope();
            var _client = scope.ServiceProvider.GetRequiredService<ISqlSugarClient>();
            _logger.LogInformation("数据库建表开始");
            try
            {
                _client.DbMaintenance.CreateDatabase();
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
                _logger.LogInformation("数据库建表完成");
            }
        }
        /// <summary>
        /// 添加种子数据
        /// </summary>
        public virtual void DbSeed(Action<ISqlSugarClient> action)
        {
            using var scope = _serviceProvider.CreateScope();
            var _client = scope.ServiceProvider.GetRequiredService<ISqlSugarClient>();
            action.Invoke(_client);
        }
    }
}
