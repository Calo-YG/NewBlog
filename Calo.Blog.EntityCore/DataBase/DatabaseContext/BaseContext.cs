﻿using Calo.Blog.EntityCore.DataBase.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.DatabaseContext
{
    public class BaseContext : SugarUnitOfWork
    {
        public BaseContext()
        {
        }
    }
}
