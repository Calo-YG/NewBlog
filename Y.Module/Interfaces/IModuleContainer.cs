﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.Module.Interfaces
{
    public interface IModuleContainer
    {
        IReadOnlyList<IYModuleDescritor> Modules { get; }
    }
}
