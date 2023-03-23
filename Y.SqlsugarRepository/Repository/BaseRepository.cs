using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Y.SqlsugarRepository.DatabaseConext;
using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Repository
{
    public class BaseRepository<TEntity, TPrimaryKey> : BaseRepository<TEntity>, IBaseRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        private readonly IServiceProvider _servicerProvider;
        private readonly IDbAopProvider _dbAopProvider;
        private readonly ILogger _logger;
        public BaseRepository(IServiceProvider provider
            , IDbAopProvider dbAopProvider
            , ILoggerFactory loggerFactory
            , ISqlSugarClient client = null) : base(provider, dbAopProvider, loggerFactory)
        {
            _servicerProvider = provider;
            _dbAopProvider = dbAopProvider;
            _logger = loggerFactory.CreateLogger(this.GetType());
            base.Context = _servicerProvider.GetRequiredService<ISqlSugarClient>();
        }
    }

    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity>
        where TEntity : class, new()
    {
        private readonly IServiceProvider _servicerProvider;
        private readonly IDbAopProvider _dbAopProvider;
        private readonly ILogger _logger;
        public BaseRepository(IServiceProvider provider
            , IDbAopProvider dbAopProvider
            , ILoggerFactory loggerFactory
            , ISqlSugarClient client = null) : base(client)
        {
            _servicerProvider = provider;
            _dbAopProvider = dbAopProvider;
            _logger = loggerFactory.CreateLogger(this.GetType());
            base.Context = _servicerProvider.GetRequiredService<ISqlSugarClient>();
            InitFilter();
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

        private void InitFilter()
        {
            var entityContianer = _servicerProvider.GetRequiredService<IEntityContainer>();
            var entityTypes = entityContianer.EntityTypes;

            foreach (var type in entityTypes)
            {
                var lambda = DynamicExpressionParser.ParseLambda
                                         (new[] { Expression.Parameter(type, "p") },
                                          typeof(bool), $"IsDeleted ==  @0",
                                           false);
                base.Context.QueryFilter.AddTableFilter(type, lambda);
            }
        }
    }
}
