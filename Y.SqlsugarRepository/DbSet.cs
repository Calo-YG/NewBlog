﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository
{
    public class DbSet<T> : IDbSet<T>
    {
        public T Entity { get ; set; }
    }
}
