using Calo.Blog.EntityCore.DataBase.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Y.SqlsugarRepository.Repository;

namespace Calo.Blog.Common.UserSession
{
    public class CurrentUserSession : IUserSession
    {
        public string? UserId { get; private set; }
        public string? UserName { get; private set; }
        public IEnumerable<string>? RoleName { get; private set; }
        public IEnumerable<long>? RoleIds { get; private set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IBaseRepository<User, Guid> _userRepository;

        public CurrentUserSession(IHttpContextAccessor httpContextAccessor, IBaseRepository<User, Guid> userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            //SetUserInfo();
        }

        public virtual void SetUserInfo()
        {
            var identity = _httpContextAccessor?.HttpContext?.User?.Identity;
            var httpContext = _httpContextAccessor?.HttpContext;
            var isAuthenticated = identity?.IsAuthenticated ?? false;
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var userId = claims?.FirstOrDefault(p => p.Type == "Id")?.Value;
            if (userId is null || !isAuthenticated)
            {
                NotLoginExceptionsExtensions.ThrowNotloginExceptions();
            }
            UserId = userId;
            UserName = claims?.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value ?? "";
            RoleIds = claims?
                      .Where(p => p?.Type?.Equals("RoleIds") ?? false)
                      .Select(p => long.Parse(p.Value));
            RoleName = claims?
                   .Where(p => p?.Type?.Equals(ClaimTypes.Role) ?? false)
                   .Select(p => p.Value);
        }
    }
}
