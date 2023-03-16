using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AjaxResponse
{
    public interface IActionResultWrapFactory
    {
        IActionResultWarp CreateContext(FilterContext filterContext);
    }
}
