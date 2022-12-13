using Calo.Blog.EntityCore.DataBase.EntityBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Repository
{
    public class CaloBaseRepository<TDbcontext,TEntity,TPrimaryKey>:BaseRepository<TEntity,TPrimaryKey>
        where TDbcontext: SugarUnitOfWork
        where TEntity: class,IEntity<TPrimaryKey>,new()
    {
        public CaloBaseRepository(ISugarUnitOfWork<TDbcontext> unitOfWork,ISqlSugarClient client = null) : base(client)
        {

        }
    }
}
