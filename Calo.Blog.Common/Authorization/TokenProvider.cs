using Microsoft.Extensions.Configuration;
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
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(jwtsetting.ExpMinutes)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(jwtsetting.ExpMinutes).ToString()),
            };
            if (user.RoleIds != null && user.RoleIds.Any())
            {
                claims.AddRange(user.RoleIds.Select(p => new Claim("RoleIds", p.ToString())));
            }
            if(user.RoleNames!= null && user.RoleNames.Any())
            {
                claims.AddRange(user.RoleNames.Select(p=>new Claim(ClaimTypes.Role, p)));
            }

            user.Claims = claims.ToArray();

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
                DateTime.Now.AddMinutes(jwtsetting.ExpMinutes),    //expires
                signingCredentials               //Credentials
            );

            // 6. 将token变为string
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
        
        /// <summary>
        /// 解析jwt字符串
        /// </summary>
        public virtual JwtAnalysis AnalysisJwt(string jwt)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtAn = new JwtAnalysis();

            if (!string.IsNullOrEmpty(jwt) && jwtHandler.CanReadToken(jwt))
            {

                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwt);
                var expraion  = jwtToken.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Expiration) ?? null;

                DateTime datetime;
                
                DateTime.TryParse(expraion?.Value ?? "", out datetime);

                jwtAn.ExprationTime= datetime;
            }
            return jwtAn;
        }

        public virtual string GenerateRefreshToken(UserTokenModel user)
        {
            var jwtsetting = _configuration.GetSection("App:JwtSetting").Get<JwtSetting>() ?? throw new ArgumentException("请先检查JWT配置");
            // 1. 定义需要使用到的Claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName), //HttpContext.User.Identity.Name
                new Claim("Id", user.UserId.ToString()),
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(jwtsetting.ExpMinutes)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(jwtsetting.ExpMinutes).ToString()),
            };
            if (user.RoleIds != null && user.RoleIds.Any())
            {
                claims.AddRange(user.RoleIds.Select(p => new Claim("RoleIds", p.ToString())));
            }
            if (user.RoleNames != null && user.RoleNames.Any())
            {
                claims.AddRange(user.RoleNames.Select(p => new Claim(ClaimTypes.Role, p)));
            }

            user.Claims = claims.ToArray();

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
                DateTime.Now.AddMinutes(jwtsetting.ExpMinutes),    //expires
                signingCredentials               //Credentials
            );

            // 6. 将token变为string
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
