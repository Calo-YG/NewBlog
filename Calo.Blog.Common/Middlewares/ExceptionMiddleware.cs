using Calo.Blog.Common.CustomOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime.ExceptionServices;
using Calo.Blog.Extenions.AjaxResponse;

namespace Calo.Blog.Common.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IOptions<ExceptionOptions> _options;

        public ExceptionMiddleware(RequestDelegate next
            , ILoggerFactory loggerFactory
            , IOptions<ExceptionOptions> options)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            ExceptionDispatchInfo edi;
            try
            {
                var task = _next(context);
                if (!task.IsCompletedSuccessfully)
                {
                    await Awaited(context, () => task);
                }
                return;
            }
            catch (Exception ex)
            {
                edi = ExceptionDispatchInfo.Capture(ex);
            }
            if (_options.Value.UseDataBase)
            {
                var record = RecordInDatabase(context);
                await Awaited(context,()=>record);

            }
            await HandlerException(context, edi);
        }

        private async Task RecordInDatabase(HttpContext context)
        {
            await Awaited(context, () => Task.CompletedTask);
        }

        private async Task HandlerException(HttpContext context, ExceptionDispatchInfo edi)
        {
            if (context.Response.HasStarted)
            {
                // 响应开始抛出异常终止响应
                edi.Throw();
            }
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var requestPath = context.Request.Path;
            var exception = edi.SourceException;
            var logMsg = $"SourseRoute {requestPath} {exception.Source} {exception.Message} {exception.StackTrace} ";
            _logger.LogError(logMsg);
            var response = new AjaxResponseResult();
            response.Success = false;
            response.StatusCode = "500";
            ErrorInfo error = new ErrorInfo();
            response.Error = error;
            error.Error = exception.Message;
            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task Awaited(HttpContext context, Func<Task> func)
        {
            ExceptionDispatchInfo? edi = null;
            try
            {
                await func.Invoke();
            }
            catch (Exception exception)
            {
                edi = ExceptionDispatchInfo.Capture(exception);
            }
            if (edi != null)
            {
                await HandlerException(context, edi);
            }
        }
    }
}
