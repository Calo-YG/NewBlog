using Calo.Blog.EntityCore.DataBase.Entities;
using SqlSugar;
using System;
using System.Threading.Tasks;
using Y.SqlsugarRepository.Repository;

namespace Calo.Blog.EntityCore.DadaSeed
{
    public class UserSeed
    {
        private readonly IBaseRepository<User,Guid> baseRepository;

        public UserSeed(IBaseRepository<User, Guid> userRepository)
        {
           baseRepository = userRepository;
        }   

        public async Task Create()
        {
            var user = new User();
            user.BirthDate = DateTime.Now;
            user.Email = "31645222062@qq.com";
            user.Password = "wyg154511";
            user.UserName = "wyg文eee";
            var isExists = baseRepository.AsQueryable().Any(p => p.UserName.Equals(user.UserName));
            if (!isExists)
            {
               await baseRepository.InsertAsync(user);
            } 
        }
    }
}
