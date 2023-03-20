using Calo.Blog.Extenions.AjaxResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Calo.Blog.Common.Authorization
{
    public class AuthorizeMiddleHandle : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddleWareResultHadnler defaultHandler = new();
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (!authorizeResult.Succeeded || authorizeResult.Challenged)
            {
                //var ajaxResponse = new AjaxResponse();
                //ErrorInfo error = new ErrorInfo();
                //ajaxResponse.UnAuthorizedRequest = true;
                //ajaxResponse.Success = false;
                //context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                //error.Error = "你没有权限访问该接口";
                //ajaxResponse.Error = error;
                //if (authorizeResult.Forbidden)
                //{
                //    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                //    error.Error = "该接口禁止访问";
                //}
                //var responseStr = JsonConvert.SerializeObject(ajaxResponse);
                //byte[] array = Encoding.UTF8.GetBytes(responseStr);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
