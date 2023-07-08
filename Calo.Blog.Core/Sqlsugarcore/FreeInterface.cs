using System;
using Y.SqlsugarRepository.EntityAttribute;

namespace Calo.Blog.Domain.Sqlsugarcore
{
    public class FreeInterface
    {
        [PrimaryKey]
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        [StringAtrribute(50)]
        public virtual string Name { get; set; }
        /// <summary>
        /// 接口描述
        /// </summary>
        [StringAtrribute(200)]
        public virtual string Description { get; set; }
        /// <summary>
        /// 接口来源
        /// </summary>
        [StringAtrribute(100)]
        public virtual string Source { get; set; }
        /// <summary>
        /// 接口url
        /// </summary>
        [StringAtrribute(100)]
        public virtual string Url { get; set; }
        /// <summary>
        /// 是否本地资源
        /// </summary>
        public virtual bool IsLocal { get; set; }
        /// <summary>
        /// 图片资源
        /// </summary>
        [StringAtrribute(100)]
        public virtual string Image { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }
}
