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
    public class YBaseRepository<TUnitOfWork, TEntity, TPrimaryKey> : BaseRepository<TEntity, TPrimaryKey>
            where TUnitOfWork : SugarUnitOfWork, new()
            where TEntity :class, IEntity<TPrimaryKey>, new()
    {
        private readonly ISugarUnitOfWork<TUnitOfWork> _unitOfWork;
        public YBaseRepository(ISugarUnitOfWork<TUnitOfWork> unitOfWork
            , IServiceProvider provider
            , IDbAopProvider dbAopProvider
            , ILogger<YBaseRepository<TUnitOfWork, TEntity, TPrimaryKey>> logger
            , ISqlSugarClient client = null) : base(provider, dbAopProvider, logger, client)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
