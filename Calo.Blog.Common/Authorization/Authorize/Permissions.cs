using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Authorization.Authorize
{
    public class Permissions
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public List<Permissions> Children { get; set; }
    }
}
