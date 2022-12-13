using Calo.Blog.EntityCore.DataBase.EntityBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Repository
{
    public class BaseRepository<TEntity,TPrimaryKey>: SimpleClient<TEntity> ,IBaseRepository<TEntity,TPrimaryKey> 
        where TEntity : class,IEntity<TPrimaryKey>,new()
    {
        public BaseRepository(ISqlSugarClient client = null) : base(client)
        {
        }

        
    }
}
