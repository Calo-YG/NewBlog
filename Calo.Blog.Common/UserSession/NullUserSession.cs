using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.UserSession
{
    public class NullUserSession:IUserSession
    {
        public static NullUserSession Instance { get; } = new NullUserSession();
        public string? UserName { get; private set; }

        public string? UserId { get; private set; }

        public IEnumerable<string>? RoleName => null;

        public IEnumerable<long>? RoleIds => null;

        public NullUserSession() { }

        public void SetUserInfo()
        {
            throw new NotImplementedException();
        }
    }
}
