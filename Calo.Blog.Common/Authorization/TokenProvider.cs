using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.ConstrainedExecution;
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
        private static DateTime? GetSafeDateTime(DateTime dateTime)
        {
            // Assigning DateTime.MinValue or default(DateTime) to a DateTimeOffset when in a UTC+X timezone will throw
            // Since we don't really care about DateTime.MinValue in this case let's just set the field to null
            if (dateTime == DateTime.MinValue)
            {
                return null;
            }
            return dateTime;
        }
        private bool CheckToken(MessageReceivedContext context,string? token,bool isrefresh)
        {
            if (string.IsNullOrEmpty(token))
            {
                context.Fail("No SecurityTokenValidator available for token.");
            }
            var validationParameters = Options.TokenValidationParameters.Clone();
            List<Exception>? validationFailures = null;
            SecurityToken? validatedToken = null;
            ClaimsPrincipal principal=null;
            if (!string.IsNullOrEmpty(token))
            {
                string authorization =context.HttpContext.Request.Headers.Authorization;

                // If no authorization header found, nothing to process further
                //if (string.IsNullOrEmpty(authorization))
                //{
                //    return AuthenticateResult.NoResult();
                //}

                if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = authorization.Substring("Bearer ".Length).Trim();
                }

                // If no token found, no further work possible
                //if (string.IsNullOrEmpty(token))
                //{
                //    return AuthenticateResult.NoResult();
                //}
            }
            foreach (var validator in Options.SecurityTokenValidators)
            {
                if (validator.CanReadToken(token))
                {
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
            var tokenValidatedContext = new TokenValidatedContext(context.HttpContext, context.Scheme, Options)
            {
                Principal = principal,
                SecurityToken = validatedToken
            };
            if(token is not null)
            {
                tokenValidatedContext.Properties.ExpiresUtc = GetSafeDateTime(validatedToken.ValidTo);
                tokenValidatedContext.Properties.IssuedUtc = GetSafeDateTime(validatedToken.ValidFrom);
            }
            return true;
        }
    }
}
