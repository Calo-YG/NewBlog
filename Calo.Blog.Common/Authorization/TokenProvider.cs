using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Calo.Blog.Common.Authorization
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _configuration;

        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public virtual string GenerateToken(UserTokenModel user)
        {
            var jwtsetting = _configuration.GetSection("App:JwtSetting").Get<JwtSetting>() ?? throw new ArgumentException("请先检查JWT配置");
            // 1. 定义需要使用到的Claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName), //HttpContext.User.Identity.Name
                new Claim("Id", user.UserId.ToString()),
            };
            if (user.RoleIds != null && user.RoleIds.Any())
            {
                claims.AddRange(user.RoleIds.Select(p => new Claim("RoleIds", p)));
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
                DateTime.Now,                    //notBefore
                DateTime.Now.AddSeconds(30),    //expires
                signingCredentials               //Credentials
            );

            // 6. 将token变为string
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
