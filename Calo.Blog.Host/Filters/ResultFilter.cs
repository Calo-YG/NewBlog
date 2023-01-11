using Calo.Blog.EntityCore.DataBase.Entities;
using Calo.Blog.Extenions.AjaxResponse;
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
        private readonly ILogger _logger;
        private readonly IActionResultWrapFactory _actionResultWrapFactory;

        public ResultFilter(ILogger<ResultFilter> logger, IActionResultWrapFactory actionResultWrapFactory)
        {
            _logger = logger;
            _actionResultWrapFactory = actionResultWrapFactory;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            var controllerAttribute = context.Controller
                .GetType()?
                .CustomAttributes?
                .FirstOrDefault(p => p.AttributeType == typeof(NoResultAttribute));
            if (controllerAttribute != null)
            {
                return;
            }
            var method = action?.MethodInfo;
            var nonResult = method?
                .CustomAttributes?
                .Where(p => p.AttributeType == typeof(NoResultAttribute));
            if (nonResult is null || !nonResult.Any())
            {
                _actionResultWrapFactory.CreateContext(context).Wrap(context);
            }
        }
    }
}
