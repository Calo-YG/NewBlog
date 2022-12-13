using Calo.Blog.EntityCore.DataBase.EntityBase;
using Calo.Blog.EntityCore.DataBase.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase
{
    public class BlogRepository<TEnetity, TPrimaryKey> : CaloBaseRepository<BlogContext,TEnetity,TPrimaryKey>
        where TEnetity : class,IEntity<TPrimaryKey>,new()
    {
        public BlogRepository(ISugarUnitOfWork<BlogContext> unitOfWork,ISqlSugarClient client = null) : base(unitOfWork,client)
        {
        }
    }
}
