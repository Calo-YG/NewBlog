using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Authorization
{
    public class AuthorizationMiddleWareResultHadnler : AuthorizationMiddlewareResultHandler, IAuthorizationMiddlewareResultHandler
    {
        public new async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            await next(context);
        }
    }
}
