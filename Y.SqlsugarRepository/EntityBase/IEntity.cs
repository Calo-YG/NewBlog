﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.EntityBase
{
    public interface IEntity<TPrimarykey>
    {
        TPrimarykey Id {get;set;}
    }
}
