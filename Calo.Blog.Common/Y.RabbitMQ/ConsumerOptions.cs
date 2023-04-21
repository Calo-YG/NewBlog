using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Y.RabbitMQ
{
    public class ConsumerOptions
    {
        public int PreFetchCount { get; set; } = 1;

    }
}
