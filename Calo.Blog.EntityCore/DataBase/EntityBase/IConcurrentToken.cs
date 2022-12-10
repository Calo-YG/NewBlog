using Calo.Blog.EntityCore.DataBase.EntityAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.EntityBase
{
    public interface IConcurrentToken
    {
        [ConcurrentToken]
        public Guid ConcurrentToken { get; set; }
    }
}
