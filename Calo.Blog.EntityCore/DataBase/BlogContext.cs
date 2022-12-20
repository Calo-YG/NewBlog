using Calo.Blog.EntityCore.DataBase.DatabaseContext;
using Calo.Blog.EntityCore.DataBase.Entities;
using Calo.Blog.EntityCore.DataBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase
{
    public class BlogContext : BaseContext
    {
        public SugarDbSet<User> User {get ;set;}
        public BlogContext() : base()
        {

        }
    }
}