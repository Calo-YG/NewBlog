using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Repository
{
    public static class AutoRegisterRepository
    {
        public static RepositoryDependencyInjection Default { get; }
        static AutoRegisterRepository()
        {
            Default = new RepositoryDependencyInjection(typeof(IBaseRepository<>),typeof(IBaseRepository<,>));
        }
    }
}
