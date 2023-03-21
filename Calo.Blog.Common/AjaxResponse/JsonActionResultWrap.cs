﻿using Microsoft.AspNetCore.Mvc.Filters;
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
                var response = new AjaxResponse();
                response.Result = jsonResult.Value;
                response.Success = true;
                response.StatusCode = "200";
                jsonResult.Value = response;
            }
        }
    }
}
