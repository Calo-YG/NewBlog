using Calo.Blog.EntityCore.DataBase.EntityAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.EntityBase
{
    public class FullAutiedEntity<T> : AutiedEntity<T>, IConcurrentToken 
    {
        public bool IsDeleted { get; set; }

        public T? DeleteUserId { get; set; }
    }
}
