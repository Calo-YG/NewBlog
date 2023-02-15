﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Interfaces
{
    public interface IModuleLoad
    {
        List<IYModuleDescritor> GetYModuleDescritors(Type startModuleType, IServiceCollection services);
    }
}
