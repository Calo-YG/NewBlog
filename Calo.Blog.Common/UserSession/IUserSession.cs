namespace Calo.Blog.Common.UserSession
{
    public interface IUserSession
    {
        public long? UserId { get; }

        public string? UserName { get; }

        public IEnumerable<string>? RoleName { get; }

        public IEnumerable<long>? RoleIds { get; }
    }
}
