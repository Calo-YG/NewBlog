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
    public class YBaseRepository<TDbcontext, TEntity, TPrimaryKey> : BaseRepository<TEntity, TPrimaryKey>
            where TDbcontext : SugarUnitOfWork, new()
            where TEntity :class, IEntity<TPrimaryKey>, new()
    {
        private readonly ISugarUnitOfWork<TDbcontext> _unitOfWork;
        public YBaseRepository(ISugarUnitOfWork<TDbcontext> unitOfWork
            , IServiceProvider provider
            , IDbAopProvider dbAopProvider
            , ILogger<YBaseRepository<TDbcontext, TEntity, TPrimaryKey>> logger
            , ISqlSugarClient client = null) : base(provider, dbAopProvider, logger, client)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
