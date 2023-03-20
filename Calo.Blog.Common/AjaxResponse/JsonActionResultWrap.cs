using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResultExecutingContext = Microsoft.AspNetCore.Mvc.Filters.ResultExecutingContext;
using Microsoft.AspNetCore.Http;

namespace Calo.Blog.Extenions.AjaxResponse
{
    public class JsonActionResultWrap : IActionResultWarp
    {
        public void Wrap(FilterContext context)
        {
         
            JsonResult? jsonResult = null;

            switch (context)
            {
                case ResultExecutingContext resultExecutingContext:
                    jsonResult = resultExecutingContext.Result as JsonResult;
                    break;
            }

            if (jsonResult == null)
            {
                throw new ArgumentException("Action Result should be JsonResult!");
            }

            if (!(jsonResult.Value is AjaxResponseBase))
            {
                var unAuthorization = context.HttpContext.Response.StatusCode == StatusCodes.Status401Unauthorized;
                var response = new AjaxResponse();
                response.Result = jsonResult.Value;
                response.UnAuthorizedRequest= unAuthorization;
                var error = new ErrorInfo();
                error.Error= unAuthorization ? "你没有权限访问该接口" : "";
                response.Error = error;
                jsonResult.Value = response;
            }
        }
    }
}
