﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AjaxResponse
{
    public class ErrorInfo
    {
        public string Error { get; set; }

        public ErrorInfo(string error)
        {
            Error = error;
        }
    }
}
