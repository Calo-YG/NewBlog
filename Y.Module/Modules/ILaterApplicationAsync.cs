﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Modules
{
    public interface ILaterApplicationAsync
    {
        Task LaterInitApplicationAsync(InitApplicationContext context);
    }
}
