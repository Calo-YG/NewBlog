using Calo.Blog.EntityCore.DataBase.Entities;
using Calo.Blog.Extenions.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;

namespace Calo.Blog.Host.Filters
{
    public class ResultFilter : IResultFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger _logger;

        public ResultFilter(IHttpContextAccessor httpContextAccessor, ILogger<ResultFilter> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var method = action?.MethodInfo;
            var nonResult = method?.CustomAttributes?.Where(p => p.AttributeType == typeof(NoResultAttribute));
            if (nonResult is null || !nonResult.Any())
            {
                var result = (ObjectResult)context.Result;
                if (result is null)
                {
                    throw new ArgumentException("Action Result should be JsonResult!");
                }
                result.Value = new User()
                {

                };
            }
        }
    }
}
