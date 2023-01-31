using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AppModule
{
    public interface IPreApplicationInition
    {
        void PreApplictionInition(IServiceProvider serviceProvider);
    }
}
