using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Y.EventBus.Y.RabbitMQ
{
    public interface IRabbitMqConnection
    {
        bool IsConnected { get; }

         void TryConnect();
         IModel CreateModel();
    }
}
