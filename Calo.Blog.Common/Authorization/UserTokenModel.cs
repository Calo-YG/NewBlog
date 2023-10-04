using System.Security.Claims;

namespace Calo.Blog.Common.Authorization
{
    public class UserTokenModel
    {
        public virtual string UserName { get; set; }
        public virtual string UserId { get; set; }

        public virtual long[]? RoleIds { get; set; }

        public virtual string[]? RoleNames { get; set; }

        public virtual Claim[] Claims { get; set; }


        public UserTokenModel() { } 

        public UserTokenModel(string username,string userId)
        {

        }
    }
}
