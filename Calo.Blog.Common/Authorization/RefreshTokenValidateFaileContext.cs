using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Calo.Blog.Common.Authorization
{
    public class RefreshTokenValidateFaileContext : ResultContext<JwtBearerRefreshOptions>
    {
        public RefreshTokenValidateFaileContext(HttpContext context, AuthenticationScheme scheme, JwtBearerRefreshOptions options) : base(context, scheme, options)
        {
        }
    }
}
