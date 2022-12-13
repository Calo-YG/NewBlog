using Calo.Blog.EntityCore.DataBase.EntityAttribute;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.EntityBase
{

    public  class Entity<T>:IEntity<T> 
    {
        [KeyWithIncrement]
        public T Id { get; set; }

        public bool IsDeleted { get; set; }

        public T? DeleteUserId { get; set; }
    }
}
