using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.MongoDB.MongoAttribute
{
    public class TableAttribute:Attribute
    {
        public string Name { get;private set; }

        public TableAttribute(string name)
        {
            Name= name;
        }
    }
}
