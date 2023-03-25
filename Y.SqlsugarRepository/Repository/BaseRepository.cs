using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseRepository(IServiceProvider provider
            , IDbAopProvider dbAopProvider
            , ILoggerFactory loggerFactory
            , IHttpContextAccessor httpContextAccessor
            , ISqlSugarClient client = null) : base(provider, dbAopProvider, loggerFactory, httpContextAccessor)
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private long? UserId { get; set; }
        private string? UserName { get; set; }
        public BaseRepository(IServiceProvider provider
            , IDbAopProvider dbAopProvider
            , ILoggerFactory loggerFactory
            , IHttpContextAccessor httpContextAccessor
            , ISqlSugarClient client = null) : base(client)
        {
            _servicerProvider = provider;
            _dbAopProvider = dbAopProvider;
            _logger = loggerFactory.CreateLogger(this.GetType());
            _httpContextAccessor = httpContextAccessor;
            base.Context = _servicerProvider.GetRequiredService<ISqlSugarClient>();
            InitInfo();
            EntityService();
            InitFilter();
            InitDbAop();
        }

        public virtual void InitInfo()
        {

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

        public virtual void EntityService()
        {
            base.Context.Aop.DataExecuting = (oldValue, entityInfo) =>
            {
                var operationType = entityInfo.OperationType;

                if (entityInfo.PropertyName == "CreationTime" && operationType == DataFilterType.InsertByObject)
                {
                    entityInfo.SetValue(DateTime.Now);
                }
                if (entityInfo.PropertyName == "IsDeleted" && operationType == DataFilterType.InsertByObject)
                {
                    entityInfo.SetValue(false);
                }

                if (entityInfo.PropertyName == "UpdateTime" && operationType == DataFilterType.UpdateByObject)
                {
                    entityInfo.SetValue(DateTime.Now);
                }
            };
        }

        public virtual void InitFilter()
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

        public virtual Task DeleteAsync(TEntity entity, string? logic = "IsDelete")
        {
            var delete = base.Context.Deleteable<TEntity>(entity);
            if (logic is null)
            {
                return delete.ExecuteCommandAsync();
            }
            return delete.IsLogic().ExecuteCommandAsync(logic);
        }

        public virtual async Task BatchDeleteAsync(List<TEntity> entities,Expression<Func<TEntity,bool>>? expression = null,string? logic = "IsDelete")
        {
            var delete = base.Context.Deleteable<TEntity>(entities);
            if (expression != null)
            {
                delete.Where(expression);
            }
            if(logic != null)
            {
              await delete.IsLogic().ExecuteCommandAsync(logic);
              return;
            }
            await delete.ExecuteCommandAsync();
        }

        public new virtual Task InsertAsync(TEntity entity)
        {
            return base.Context.Insertable<TEntity>(entity).ExecuteCommandAsync();
        }

        public virtual Task<TEntity> InsertReturnEnityAsync(TEntity entity)
        {
            return base.Context.Insertable<TEntity>(entity).ExecuteReturnEntityAsync();
        }

        public virtual async Task BatchInsertAsync(List<TEntity> entitys)
        {
            await base.Context.Insertable<TEntity>(entitys).ExecuteCommandAsync();
        }

        public virtual Task BatchFastInsertAsync(List<TEntity> entities, int pageSize = 1000)
        {
            return base.Context.Fastest<TEntity>().PageSize(pageSize).BulkCopyAsync(entities);
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return base.Context.Queryable<TEntity>().WhereIF(expression != null, expression).AnyAsync();
        }

        public virtual bool Any(Expression<Func<TEntity, bool>>? expression = null)
        {
            return base.Context.Queryable<TEntity>().WhereIF(expression != null, expression).Any();
        }

        public virtual Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>>? expression = null)
        {
            var update = base.Context.Updateable<TEntity>(entity);
            if (expression == null)
            {
                return update.ExecuteCommandAsync();
            }
            return update.Where(expression).ExecuteCommandAsync();
        }

        public virtual Task BatchUpdateAsync(List<TEntity> entityes)
        {
            return base.Context.Updateable<TEntity>(entityes).ExecuteCommandAsync();
        }

        public virtual Task FastBatchUpdateAsync(List<TEntity> entities)
        {
            return base.Context.Fastest<TEntity>().BulkCopyAsync(entities);
        }

        public virtual Task UpdateColumnsAsync(TEntity entity, Expression<Func<TEntity, object>>? columns = null)
        {
            var update = base.Context.Updateable(entity);
            if (columns != null)
            {
                return update.UpdateColumns(columns).ExecuteCommandAsync();
            }
            return update.ExecuteCommandAsync();
        }

        public virtual Task UpdateColumnsAsync(TEntity entity, params string[]? columns)
        {
            var update = base.Context.Updateable(entity);
            if (columns != null)
            {
                return update.UpdateColumns(columns).ExecuteCommandAsync();
            }
            return update.ExecuteCommandAsync();
        }
    }
}
