using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace Calo.Blog.Common.Authorization.WeChatLogin
{
    public class WeChatLoginHandler : OAuthHandler<WeChatOptions>
    {
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<WeChatOptions> _options;
        public WeChatLoginHandler(IOptionsMonitor<WeChatOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _logger = logger.CreateLogger<WeChatLoginHandler>();
            _options = options;
        }

        public override Task<bool> HandleRequestAsync()
        {
            return base.HandleRequestAsync();
        }
    }
}
