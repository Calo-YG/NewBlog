using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module
{
    public class InitApplicationContext
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public InitApplicationContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
