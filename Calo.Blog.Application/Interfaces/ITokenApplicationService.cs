﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Application.Interfaces
{
    public interface ITokenApplicationService
    {
        public string GenerateToken(Guid userId);
    }
}
