namespace Calo.Blog.Common.Authorization
{
    public class UserTokenModel
    {
        public virtual string UserName { get; set; }
        public virtual long UserId { get; set; }

        public virtual string[] RoleNames { get; set; }
    }
}
