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
        where TDbcontext: SugarUnitOfWork,new()
        where TEntity: class,IEntity<TPrimaryKey>,new()
    {
        private readonly ISugarUnitOfWork<TDbcontext> _unitOfWork;
        public CaloBaseRepository(ISugarUnitOfWork<TDbcontext> unitOfWork,IServiceProvider provider,ISqlSugarClient client = null) : base(provider,client)
        {
            _unitOfWork= unitOfWork;
        }
    }
}
