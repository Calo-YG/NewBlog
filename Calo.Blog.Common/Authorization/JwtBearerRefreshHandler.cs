using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Text.Encodings.Web;

namespace Calo.Blog.Common.Authorization
{
    public class JwtBearerRefreshHandler : AuthenticationHandler<JwtBearerRefreshOptions>
    {
        private OpenIdConnectConfiguration? _configuration;

        protected new JwtBearerRefreshEvents RefreshEvents
        {
            get => (JwtBearerRefreshEvents)base.Events!;
            set => base.Events = value;
        }

        protected new  readonly JwtBearerEvents Events;

        private readonly JwtBearerRefreshOptions _options;
        public JwtBearerRefreshHandler(IOptionsMonitor<JwtBearerRefreshOptions> options
            , ILoggerFactory logger
            , UrlEncoder encoder
            , ISystemClock clock) : base(options, logger, encoder, clock)
        {
            Events = _options.JwtBearerOptions.Events;
        }

        

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
