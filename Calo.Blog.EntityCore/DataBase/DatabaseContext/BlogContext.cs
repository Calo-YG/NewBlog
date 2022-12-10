using Calo.Blog.EntityCore.DataBase.Entities;
using Calo.Blog.EntityCore.DataBase.Extensions;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.DatabaseContext
{
    public class BlogContext :SugarUnitOfWork
    {
        public BlogContext()
        { 
          
        }

        public void AddIsDeleteFilter()
        {
            
            Db.QueryFilter.ConfigureFilterForEntity();
        }
    }
}
