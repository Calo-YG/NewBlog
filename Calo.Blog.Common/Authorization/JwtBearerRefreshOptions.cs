using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Calo.Blog.Common.Authorization
{
    public class JwtBearerRefreshOptions: AuthenticationSchemeOptions
    {
        public  JwtBearerRefreshEvents RefreshEvent { get; set; }
        public JwtBearerOptions JwtBearerOptions { get; set; }
    }
}
