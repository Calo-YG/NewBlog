using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.InputEntity
{
    public class PageInputAndSort : PageInput, ISorting
    {
        public string  SortText { get; set; }

        void ISorting.CheckSort()
        {
            if (string.IsNullOrEmpty(SortText))
            {
                SortText = "creationTime desc";
            }
        }
    }
}
