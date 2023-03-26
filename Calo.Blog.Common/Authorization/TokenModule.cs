using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Authorization
{
    public class TokenModule
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
