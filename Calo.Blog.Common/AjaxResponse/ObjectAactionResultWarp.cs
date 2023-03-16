using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AjaxResponse
{
    public class ObjectAactionResultWarp : IActionResultWarp 
    {
        public void Wrap(FilterContext context)
        {
            ObjectResult? objectResult = null;

            switch (context)
            {
                case ResultExecutingContext resultExecutingContext:
                    objectResult = resultExecutingContext.Result as ObjectResult;
                    break;
            }

            if (objectResult == null)
            {
                throw new ArgumentException("Action Result should be JsonResult!");
            }

            if (!(objectResult.Value is AjaxResponseBase))
            {
                objectResult.Value = new AjaxResponse(objectResult.Value);
                objectResult.DeclaredType = typeof(AjaxResponse);
            }
        }
    }
}
