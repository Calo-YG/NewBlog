using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.InputEntity
{
    public interface ISorting
    {
        public string SortText { get; set; }

        public void CheckSort ();
    }
}
