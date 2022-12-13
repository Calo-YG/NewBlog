using Calo.Blog.EntityCore.DataBase.EntityAttribute;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.EntityBase
{

    public  class Entity<T>:IEntity<T>,IConcurrentToken
    {
        [KeyWithIncrement]
        public T Id { get; set; }
        public T? CreatorUserId { get; set; }
        public string? CreateorUserName { get; set; }

        public DateTime? CreattionTime { get; set; }

        [ConcurrentToken]
        public Guid ConcurrentToken { get; set; }
    }
}
