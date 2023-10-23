using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace Calo.Blog.Common.Authorization
{
    public class JwtBearerRefreshHandler : JwtBearerHandler
    {
        private readonly JwtBearerRefreshOptions _options;
        public JwtBearerRefreshHandler(IOptions<JwtBearerRefreshOptions> refreshOptions
            , IOptionsMonitor<JwtBearerOptions> options
            , ILoggerFactory logger
            , UrlEncoder encoder
            , ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _options = refreshOptions.Value;
        }
    }
}
