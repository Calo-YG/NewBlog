﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.Module.Interfaces;

namespace Y.Module
{
    public class ObjectAccessor<T> : IObjectAccessor<T>
    {
        public T? Value { get;  set; }
        public ObjectAccessor() { }

        public ObjectAccessor(T value) { Value = value; }
    }
}
