using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.DatabaseContext
{
    public interface IDbcontext<TDbContext> : ISugarUnitOfWork<TDbContext> where TDbContext:SugarUnitOfWork,new()
    {
    }
}
