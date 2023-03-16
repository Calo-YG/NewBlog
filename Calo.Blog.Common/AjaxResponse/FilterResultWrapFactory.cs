﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AjaxResponse
{
    public  class FilterResultWrapFactory : IActionResultWrapFactory
    {
        public  IActionResultWarp CreateContext(FilterContext filterContext)
        {
            if(filterContext == null)
            {
                throw new ArgumentNullException("ResultFilter FilterContext Is Null");
            }
            switch (filterContext)
            {
                case ResultExecutingContext result when result.Result is ObjectResult:
                     return new ObjectAactionResultWarp();
                case ResultExecutedContext result when result.Result is JsonResult:
                    return new JsonActionResultWrap();
                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is EmptyResult:
                    return new ActionEmptyResultWrap();
                default : return new NullAactionResultWrap();
            }
        }
    }
}
