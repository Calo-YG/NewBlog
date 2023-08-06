using System;
using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.Domain.Sqlsugarcore
{
    public class SourceType:Entity<Guid>
    {
        /// <summary>
        /// id
        /// </summary>
        [PrimaryKey]
        public new Guid Id { get; set; }
        [StringAtrribute(20)]
        public string Name { get; set; }
        /// <summary>
        /// 分类描述
        /// </summary>
        public string? Description { get; set; }
    }
}
