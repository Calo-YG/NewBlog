using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase
{
    public class IDbContext 
    {
        public ISqlSugarClient? DbContext;

        public ConnectionConfig? ConnectionConfig;
    }
}
