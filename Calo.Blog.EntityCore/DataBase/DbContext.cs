using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase
{
    public abstract class DbContext<TDbContext> : IDbContext where TDbContext:ISqlSugarClient
    {
        public DbContext(ConnectionConfig connectionConfig)
        {
            base.DbContext = new SqlSugarScope(ConnectionConfig);
        }
    }
}
