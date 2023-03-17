﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Authorization
{
    public class PermissionCheck : IPermissionCheck
    {
        public virtual bool IsGranted(UserTokenModel userTokenModel, string[] authorizationNames)
        {
            return false;
        }
    }
}
