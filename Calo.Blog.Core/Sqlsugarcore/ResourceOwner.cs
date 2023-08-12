using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.Domain.Sqlsugarcore
{
    public class ResourceOwner:Entity<Guid>
    {
        [PrimaryKey]
        public new  Guid Id { get; set; }
        [StringAtrribute(20)]
        public string Name { get; set; }
        [StringAtrribute]
        public string PassWord { get; set; }
        [StringAtrribute]
        public string Description { get; set; }
        /// <summary>
        /// 是否启用密钥访问
        /// </summary>
        public bool Secrect { get; set; }
        /// <summary>
        /// 一对多
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(SourceBucket.Id))]//BookA表中的studenId
        public List<SourceBucket> Buckets { get; set; }

        /// <summary>
        /// sqlsugar 原因必须创建无参构造函数
        /// </summary>
        public ResourceOwner() { }


        public ResourceOwner(string name, string password,string description,bool secrect=false)
        {
            Name = name;
            PassWord= password;
            Description = description;
            Secrect = secrect;
        }

        public ResourceOwner(Guid id,string name, string password, string description, string? concurrentToken, bool secrect = false)
        {
            Id = id;
            Name = name;
            PassWord = password;
            Description = description;
            Secrect = secrect;
            ConcurrentToken = concurrentToken;
        }
    }
}
