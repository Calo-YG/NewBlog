using Calo.Blog.EntityCore.DataBase.EntityBase;
using Calo.Blog.Extenions.InputAndOutDto;
using Calo.Blog.Extenions.InputEntity;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Records.NotUsed;

namespace Calo.Blog.EntityCore.DataBase.Repository
{
    public class BaseRepository<TEntity,TPrimaryKey>: SimpleClient<TEntity> ,IBaseRepository<TEntity,TPrimaryKey> 
        where TEntity : class,IEntity<TPrimaryKey>,new()
    {
        private readonly IServiceProvider _servicerProvider;
        public BaseRepository(IServiceProvider provider,ISqlSugarClient client = null) : base(client)
        {
            _servicerProvider = provider;
            base.Context=_servicerProvider.GetRequiredService<ISqlSugarClient>();
        }
    }
}
