using Calo.Blog.EntityCore.DataBase.EntityBase;
using Calo.Blog.EntityCore.DataBase.Extensions;
using Calo.Blog.Extenions.InputAndOutDto;
using Calo.Blog.Extenions.InputEntity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Bsi;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Records.NotUsed;

namespace Calo.Blog.EntityCore.DataBase.Repository
{
    public class BaseRepository<TEntity, TPrimaryKey> : SimpleClient<TEntity>, IBaseRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        private readonly IServiceProvider _servicerProvider;
        private readonly IOptions<DbConfigureOptions> _dbopions;
        private readonly ILogger<BaseRepository<TEntity, TPrimaryKey>> _logger;
        public BaseRepository(IServiceProvider provider
            , IOptions<DbConfigureOptions> dboptions
            , ILogger<BaseRepository<TEntity, TPrimaryKey>> logger
            , ISqlSugarClient client = null) : base(client)
        {
            _servicerProvider = provider;
            _dbopions = dboptions;
            _logger = logger;
            base.Context = _servicerProvider.GetRequiredService<ISqlSugarClient>();
        }
    }

    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity>
        where TEntity : class, new()
    {
        private readonly IServiceProvider _servicerProvider;
        private readonly IOptions<DbConfigureOptions> _dbopions;
        private readonly ILogger<BaseRepository<TEntity>> _logger;
        public BaseRepository(IServiceProvider provider
            , IOptions<DbConfigureOptions> dboptions
            , ILogger<BaseRepository<TEntity>> logger
            , ISqlSugarClient client = null) : base(client)
        {
            _servicerProvider = provider;
            _dbopions = dboptions;
            _logger = logger;
            base.Context = _servicerProvider.GetRequiredService<ISqlSugarClient>();
        }
    }
}
