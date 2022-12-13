using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.DependencyInjection.AutoFacDependencyInjection
{
    public class IocManager : IIocManager
    {
        public static ContainerBuilder container { get; private set; }

        public ContainerBuilder _container { get; set; }

        public IocManager()
        {
            _container= CreateContainer();
        }

        protected ContainerBuilder CreateContainer()
        {
            return new ContainerBuilder();
        }
        static IocManager()
        {
            container = new ContainerBuilder();
        }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}
