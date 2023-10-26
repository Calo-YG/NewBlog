
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Calo.Blog.Common.Authorization
{
    public interface ITokenProvider
    {
        (string Token, string RefreshToken) GenerateToken(UserTokenModel user);
        void CheckToken(MessageReceivedContext context);
    }
}
