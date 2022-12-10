using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.EntityAttribute
{
    public class TableAttribute:Attribute
    {
        public string Name { get => _name; }

        private string _name { get; set; }

        public TableAttribute(string name)
        {
            _name = name;
        }
    }
}
