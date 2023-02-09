using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    public interface IYModule
    {
        void ServiceConfiguration(ServiceConfigurationContext context);
        void ApplictionInit(IServiceProvider serviceProvider);
    }
}
