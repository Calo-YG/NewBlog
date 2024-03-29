﻿using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.EntityCore.DataBase.Entities
{
    public class UserBase : FullAutiedEntity<Guid>
    {
        [StringAtrribute]
        public string UserName { get; set; }

        public string Password { get; set; }

        [StringAtrribute(length:50)]
        public string? Email { get; set; }   

        public string? Phone { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
