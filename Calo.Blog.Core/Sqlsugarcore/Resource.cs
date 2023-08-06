using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.Domain.Sqlsugarcore
{
    public class Resource:Entity<Guid>
    {
        /// <summary>
        /// 资源id
        /// </summary>
        public new Guid Id { get; set; }
        /// <summary>
        /// 存储桶
        /// </summary>
        public Guid SourceBucketId { get; set; }
        /// <summary>
        /// 资源拥有者iD
        /// </summary>
        public Guid OwnerId { get; set; }
        /// <summary>
        /// 分类id
        /// </summary>
        public Guid SourceTypeId { get; set; }    
        /// <summary>
        /// 资源url
        /// </summary>
        [StringAtrribute(200)]
        public string Url { get; set; }
        /// <summary>
        /// 资源描述
        /// </summary>
        [StringAtrribute(200)]
        public string Description { get; set; }
        [StringAtrribute(20)]
        public string Name { get; set; }
    }
}
