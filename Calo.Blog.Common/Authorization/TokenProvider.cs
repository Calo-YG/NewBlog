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

            //7.生成refreshtoken
            jwtSecurityToken = new JwtSecurityToken(jwtsetting.Issuer,     //Issuer
                jwtsetting.Audience,   //Audience
                claims,                          //Claims,
                DateTime.Now,                    //notBefore
                DateTime.Now.AddMinutes(jwtsetting.ExpMinutes+10),    //expires
                signingCredentials);
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return (token,refreshToken);
        }

        public void CheckToken(MessageReceivedContext context)
        {
            var accessToken = context.Request.Headers["Authorization"];
            var refreshToken = context.Request.Headers["RefreshToken"];

            var pass = CheckToken(context,accessToken, false);
            //token验证失败
            if(!pass)
            {
                //验证refreshtoken
                CheckToken(context,refreshToken, true);
            }

        }

        public bool CheckToken(MessageReceivedContext context,string? token,bool isrefresh)
        {
            if (string.IsNullOrEmpty(token))
            {
                context.Fail("No SecurityTokenValidator available for token.");
            }
            var validationParameters = Options.TokenValidationParameters.Clone();
            List<Exception>? validationFailures = null;
            SecurityToken? validatedToken = null;
            foreach (var validator in Options.SecurityTokenValidators)
            {
                if (validator.CanReadToken(token))
                {
                    ClaimsPrincipal principal;
                    try
                    {
                        principal = validator.ValidateToken(token, validationParameters, out validatedToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning("Token 验证异常");

                        // Refresh the configuration for exceptions that may be caused by key rollovers. The user can also request a refresh in the event.
                        if (Options.RefreshOnIssuerKeyNotFound && Options.ConfigurationManager != null
                            && ex is SecurityTokenSignatureKeyNotFoundException)
                        {
                            Options.ConfigurationManager.RequestRefresh();
                        }

                        if (validationFailures == null)
                        {
                            validationFailures = new List<Exception>(1);
                        }
                        validationFailures.Add(ex);
                        continue;
                    }
                }
            }

            if (validationFailures?.Any() ?? false)
            {
                if (isrefresh)
                {
                    var authenticationFailedContext = new AuthenticationFailedContext(context.HttpContext, context.Scheme, Options)
                    {
                        Exception = (validationFailures.Count == 1) ? validationFailures[0] : new AggregateException(validationFailures)
                    };
                    context.Fail(authenticationFailedContext.Exception);
                }
                return false;
            }
            return true;
        }
    }
}
