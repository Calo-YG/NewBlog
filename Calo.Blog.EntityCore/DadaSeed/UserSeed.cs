using Calo.Blog.EntityCore.DataBase.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DadaSeed
{
    public class UserSeed
    {
        private readonly ISqlSugarClient _client;

        public UserSeed(ISqlSugarClient client)
        {
            _client = client;
        }   

        public void Create()
        {
            var user = new User();
            user.BirthDate = DateTime.Now;
            user.Email = "31645222062@qq.com";
            user.Password = "wyg154511";
            user.UserName = "wyg";
            var isExists = _client.Queryable<User>().Any(p => p.UserName.Equals(user.UserName));
            if (!isExists)
            {
                _client.Insertable<User>(user).ExecuteCommand();
            } 
        }
    }
}
