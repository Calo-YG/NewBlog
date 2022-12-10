using Calo.Blog.EntityCore.DataBase.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Entities
{
    public class UserBase<T>:FullAutiedEntity<T> where T : new()
    {
    }
}
