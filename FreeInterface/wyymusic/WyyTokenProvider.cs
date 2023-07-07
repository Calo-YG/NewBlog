using System;
using WebApiClientCore.Extensions.OAuths;
using WebApiClientCore.Extensions.OAuths.TokenProviders;

namespace FreeInterface.wyymusic
{
    public class WyyTokenProvider : TokenProvider
    {
        public WyyTokenProvider(IServiceProvider services) : base(services)
        {
        }

        protected override Task<TokenResult?> RefreshTokenAsync(IServiceProvider serviceProvider, string refresh_token)
        {
            return this.RefreshTokenAsync(serviceProvider, refresh_token);
        }

        protected override Task<TokenResult?> RequestTokenAsync(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
