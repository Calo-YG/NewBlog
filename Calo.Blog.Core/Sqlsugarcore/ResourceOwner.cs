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
    }
}
