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
        public static ContainerBuilder containerBuilder { get; private set; }

        public ContainerBuilder _container { get; set; }

        public IContainer container { get => _container.Build(); }

        public IocManager()
        {
            _container= CreateContainer();
        }

        protected ContainerBuilder CreateContainer()
        {
            return new ContainerBuilder();
        }

        public virtual bool IsRegistered(Type type)
        {
            using(var scope = container.BeginLifetimeScope())
            {
                var depeninType = scope.Resolve(type);
                return depeninType == null;
            }
        }

        public virtual bool IsRegistered<TType>() where TType : notnull
        {
            using (var scope = container.BeginLifetimeScope())
            {
                var depeninType = scope.Resolve<TType>();
                return depeninType == null;
            }
        }
        static IocManager()
        {
            containerBuilder = new ContainerBuilder();
        }
        public void Dispose()
        {
            this.Dispose();
        }
    }
}
