using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Calo.Blog.Common.Authorization
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;

        private readonly JwtBearerOptions Options;

        private readonly ILogger _logger;

        public TokenProvider(IConfiguration configuration
            , IOptionsMonitor<JwtBearerOptions> options
            , ILoggerFactory factory)
        {
            _configuration = configuration;
            Options = options.CurrentValue;
            _logger = factory.CreateLogger<ITokenProvider>();
        }
        public virtual (string Token,string RefreshToken) GenerateToken(UserTokenModel user)
        {
            var jwtsetting = _configuration.GetSection("App:JwtSetting").Get<JwtSetting>() ?? throw new ArgumentException("请先检查JWT配置");
            var now = DateTime.Now;
            // 1. 定义需要使用到的Claims
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName), //HttpContext.User.Identity.Name
                new Claim("Id", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Iss,jwtsetting.Issuer),
                new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(now).ToUnixTimeSeconds().ToString()),
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(now.AddMinutes(jwtsetting.ExpMinutes)).ToUnixTimeSeconds().ToString()}"),
            };

            if(user?.Claims?.Any() ?? false)
            {
                claims.AddRange(user.Claims);
            }

            // 2. 从 appsettings.json 中读取SecretKey
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsetting.SecretKey));

            // 3. 选择加密算法
            var algorithm = SecurityAlgorithms.HmacSha256;

            // 4. 生成Credentials
            var signingCredentials = new SigningCredentials(secretKey, algorithm);

            // 5. 根据以上，生成token
            var jwtSecurityToken = new JwtSecurityToken(
                jwtsetting.Issuer,     //Issuer
                jwtsetting.Audience,   //Audience
                claims,                          //Claims,
                now,                    //notBefore
                now.AddMinutes(jwtsetting.ExpMinutes),    //expires
                signingCredentials               //Credentials
            );

            // 6. 将token变为string
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            //7.生成refreshtoken
            jwtSecurityToken = new JwtSecurityToken(jwtsetting.Issuer,     //Issuer
                jwtsetting.Audience,   //Audience
                claims,                          //Claims,
                now,                    //notBefore
                now.AddMinutes(jwtsetting.ExpMinutes+10),    //expires
                signingCredentials);
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return (token,refreshToken);
        }
    }
}
