using Calo.Blog.EntityCore.DataBase.EntityBase;
using Calo.Blog.Extenions.DependencyInjection.LifeTimeMethods;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Repository
{
    public interface IBaseRepository<TEntity,TPrimaryKey> :IDependencyInjectionTransit 
        where TEntity : class,IEntity<TPrimaryKey>
    {
    }
}
