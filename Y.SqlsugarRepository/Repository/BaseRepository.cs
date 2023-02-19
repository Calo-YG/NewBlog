using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Repository
{
    public class BaseRepository<TEntity, TPrimaryKey> : SimpleClient<TEntity>, IBaseRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        private readonly IServiceProvider _servicerProvider;
        private readonly IDbAopProvider _dbAopProvider;
        private readonly ILogger<BaseRepository<TEntity, TPrimaryKey>> _logger;
        public BaseRepository(IServiceProvider provider
            , IDbAopProvider dbAopProvider
            , ILogger<BaseRepository<TEntity, TPrimaryKey>> logger
            , ISqlSugarClient client = null) : base(client)
        {
            _servicerProvider = provider;
            _dbAopProvider = dbAopProvider;
            _logger = logger;
            base.Context = _servicerProvider.GetRequiredService<ISqlSugarClient>();
            InitDbAop();
        }

        private void InitDbAop()
        {
            if (_dbAopProvider.DbConfigureOptions.EnableAopLog)
            {
                base.Context.Aop.OnLogExecuting = _dbAopProvider.AopLogAction(_logger);
            }
            if (_dbAopProvider.DbConfigureOptions.EnableAopError)
            {
                base.Context.Aop.OnError = _dbAopProvider.AopErrorAction(_logger);
            }
        }
    }

    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity>
        where TEntity : class, new()
    {
        private readonly IServiceProvider _servicerProvider;
        private readonly IDbAopProvider _dbAopProvider;
        private readonly ILogger<BaseRepository<TEntity>> _logger;
        public BaseRepository(IServiceProvider provider
            , IDbAopProvider dbAopProvider
            , ILogger<BaseRepository<TEntity>> logger
            , ISqlSugarClient client = null) : base(client)
        {
            _servicerProvider = provider;
            _dbAopProvider = dbAopProvider;
            _logger = logger;
            base.Context = _servicerProvider.GetRequiredService<ISqlSugarClient>();
            InitDbAop();
        }

        private void InitDbAop()
        {
            if (_dbAopProvider.DbConfigureOptions.EnableAopLog)
            {
                base.Context.Aop.OnLogExecuting = _dbAopProvider.AopLogAction(_logger);
            }
            if (_dbAopProvider.DbConfigureOptions.EnableAopError)
            {
                base.Context.Aop.OnError = _dbAopProvider.AopErrorAction(_logger);
            }
        }
    }
}
