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
        void ServiceConfiguration(IServiceCollection services);
        void ApplictionInit(IServiceProvider serviceProvider);
    }
}
