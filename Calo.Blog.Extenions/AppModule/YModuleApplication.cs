using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    internal class YModuleApplication : IModuleApplication
    {
       public Type StartModuleType { get; private set; }

       public IServiceCollection Services { get; private set; }
           
       public IServiceProvider Provider { get; private set; }

       public  IReadOnlyList<IYModule> Modules { get;private set; } 
       
       public IEnumerable<IYModule> Source { get; private set; }

        public YModuleApplication(Type startype , IServiceCollection services)
        {
            StartModuleType = startype;
            Services = services;
            SetServiceProvider();
            _=services.AddSingleton<IModuleApplication>(this);
            _ = services.TryAddIObjectAccessor<IServiceProvider>();
            LoadModules();
        }

        private void SetServiceProvider()
        {
            using var scope= Services.BuildServiceProvider().CreateScope();
            Provider= scope.ServiceProvider;
        }

        public virtual void LoadModules()
        {

        }
    }
}
