using Calo.Blog.Extenions.DependencyInjection.LifeTimeMethods;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Repository
{
    public interface IBaseRepository<TDbcontext,T>:IDependencyInjectionTransit where TDbcontext:SugarUnitOfWork,new() where T:class,new()
    {

    }
}
