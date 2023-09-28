using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System.Collections.Concurrent;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
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
            , IPropriesSetValue propriesSetValue
            , ISqlSugarClient client = null) : base(provider, dbAopProvider, loggerFactory, httpContextAccessor,propriesSetValue)
        {
        }
    }

    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity>
        where TEntity : class, new()
    {
        private readonly IServiceProvider _servicerProvider;
        private readonly IDbAopProvider _dbAopProvider;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPropriesSetValue _propriesSetValue;
        private long? UserId { get; set; }
        private string? UserName { get; set; }

        public ConcurrentBag<DataExecutingTrigger> DataExecutingTriggers { get;private set; }
        public BaseRepository(IServiceProvider provider
            , IDbAopProvider dbAopProvider
            , ILoggerFactory loggerFactory
            , IHttpContextAccessor httpContextAccessor
            , IPropriesSetValue propriesSetValue
            , ISqlSugarClient client = null) : base(client)
        {
            _servicerProvider = provider;
            _dbAopProvider = dbAopProvider;
            _logger = loggerFactory.CreateLogger(this.GetType());
            _httpContextAccessor = httpContextAccessor;
            _propriesSetValue = propriesSetValue;
            base.Context = _servicerProvider.GetRequiredService<ISqlSugarClient>();
            EntityService();
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


        public virtual void EntityService()
        {
            base.Context.Aop.DataExecuting = (oldValue, entityInfo) =>
            {
                var operationType = entityInfo.OperationType;

                foreach(var trigger in _propriesSetValue.DataExecutingTriggers)
                {
                    if (entityInfo.PropertyName == trigger.Property && operationType == trigger.FilterType)
                    {
                        entityInfo.SetValue(trigger.Func.Invoke());
                    }
                }
            };
        }

        public virtual void InitFilter()
        {
            foreach (var filter in _propriesSetValue.Filters)
            {
                base.Context.QueryFilter.AddTableFilter(filter.EntityType, filter.LambdaExpression);
            }
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            var delete = base.Context.Deleteable<TEntity>(entity);

            var deleteStr = "IsDeleted";

            var hasDelete = typeof(TEntity).GetProperties().Any(p => p.Name.Equals(deleteStr));

            if (!hasDelete)
            {
                return delete.ExecuteCommandAsync();
            }
            return delete.IsLogic().ExecuteCommandAsync(deleteStr);
        }

        public virtual async Task BatchDeleteAsync(List<TEntity> entities,Expression<Func<TEntity,bool>>? expression = null)
        {
            var delete = base.Context.Deleteable<TEntity>(entities);

            var deleteStr = "IsDeleted";

            var hasDelete = typeof(TEntity).GetProperties().Any(p => p.Name.Equals(deleteStr));

            if (expression != null)
            {
                delete.Where(expression);
            }
            if(hasDelete)
            {
              await delete.IsLogic().ExecuteCommandAsync(deleteStr);
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

        public virtual async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>>? expression = null)
        {
            var update = base.Context.Updateable<TEntity>(entity);

            var deleteStr = "ConcurrentToken";

            var concurrentToke = typeof(TEntity).GetProperties().Any(p => p.Name.Equals(deleteStr));

            if (concurrentToke && expression is not null)
            {
                await update.Where(expression).ExecuteCommandWithOptLockAsync();
                return;
            }
            if (concurrentToke)
            {
                await update.ExecuteCommandWithOptLockAsync();
                return;
            }
            if (expression == null)
            {
                await update.ExecuteCommandAsync();
                return;
            }
            await update.Where(expression).ExecuteCommandAsync();
        }

        public virtual Task BatchUpdateAsync(List<TEntity> entityes)
        {
            return base.Context.Updateable<TEntity>(entityes).ExecuteCommandAsync();
        }

        public virtual Task FastBatchUpdateAsync(List<TEntity> entities)
        {
            return base.Context.Fastest<TEntity>().BulkCopyAsync(entities);
        }

        public virtual async Task UpdateColumnsAsync(TEntity entity, Expression<Func<TEntity, object>>? columns = null)
        {
            var update = base.Context.Updateable(entity);
            var deleteStr = "ConcurrentToken";

            var concurrentToke = typeof(TEntity).GetProperties().Any(p => p.Name.Equals(deleteStr));
            if (concurrentToke && columns is not null)
            {
                await update.UpdateColumns(columns).ExecuteCommandWithOptLockAsync();
                return;
            }
            if (concurrentToke)
            {
                await update.ExecuteCommandWithOptLockAsync();
                return;
            }
            if (columns == null)
            {
                await update.ExecuteCommandAsync();
                return;
            }
            await update.UpdateColumns(columns).ExecuteCommandAsync();
        }

        public virtual async Task UpdateColumnsAsync(TEntity entity, params string[]? columns)
        {
            var update = base.Context.Updateable(entity);
            var deleteStr = "ConcurrentToken";

            var concurrentToke = typeof(TEntity).GetProperties().Any(p => p.Name.Equals(deleteStr));
            if (concurrentToke && columns is not null)
            {
                await update.UpdateColumns(columns).ExecuteCommandWithOptLockAsync();
                return;
            }
            if (concurrentToke)
            {
                await update.ExecuteCommandWithOptLockAsync();
                return;
            }
            if (columns == null)
            {
                await update.ExecuteCommandAsync();
                return;
            }
            await update.UpdateColumns(columns).ExecuteCommandAsync();
        }
    }
}
