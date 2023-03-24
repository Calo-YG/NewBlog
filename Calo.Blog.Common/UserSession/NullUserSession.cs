using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.UserSession
{
    public class NullUserSession
    {
        public static NullUserSession Instance { get; } = new NullUserSession();
        public string? UserName { get; private set; }

        public long? UserId { get; private set; }
        public NullUserSession() { }
    }
}
