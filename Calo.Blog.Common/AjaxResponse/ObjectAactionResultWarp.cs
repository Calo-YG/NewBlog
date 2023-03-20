using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
                var unAuthorization = context.HttpContext.Response.StatusCode == StatusCodes.Status401Unauthorized;
                var response = new AjaxResponse();
                response.Result = objectResult.Value;
                response.UnAuthorizedRequest = unAuthorization;
                var error = new ErrorInfo();
                error.Error = unAuthorization ? "你没有权限访问该接口" : "";
                response.Error = error;
                objectResult.Value = response;
                objectResult.DeclaredType = typeof(AjaxResponse);
            }
        }
    }
}
