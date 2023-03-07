using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository
{
    public interface IDbSet<T>:ISugarQueryable<T>
    {
        public T Entity { get; set; }
    }
}
