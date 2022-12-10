using Calo.Blog.EntityCore.DataBase.EntityAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Entities
{
    [Table("User")]
    public class User :UserBase<long>
    {
    }
}
