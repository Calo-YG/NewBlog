﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Y.EventBus.Y.RabbitMQ
{
    public interface IRabbitEventBus:IEventBus
    {
        public Task PublishAsync(Object message);
    }

   
}
