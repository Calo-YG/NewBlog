using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Repository
{
    public class RepositoryDependencyInjection
    {
        public Type RepositoryInterface { get; }

        public Type RepositoryInterfaceWithPrimaryKey { get; }

        //public Type RepositoryImplementation { get; }

        //public Type RepositoryImplementationWithPrimaryKey { get; }

        public RepositoryDependencyInjection(Type repositoryInterface,
            Type repositoryInterfaceWithPrimaryKey)
            //Type repositoryImplementation,
            //Type repositoryImplementationWithPrimaryKey)
        {
            RepositoryInterface = repositoryInterface;
           RepositoryInterfaceWithPrimaryKey = repositoryInterfaceWithPrimaryKey;
        //    RepositoryImplementation = repositoryImplementation;
        //    RepositoryImplementationWithPrimaryKey = repositoryImplementationWithPrimaryKey;
        }
    }
}
