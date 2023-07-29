using SqlSugar;
using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.Domain.Sqlsugarcore
{
    public class SourceBucket:Entity<Guid>
    {
        public new Guid Id { get; set; }
        /// <summary>
        /// 资源拥有者id
        /// </summary>
        public Guid OwnerId { get; set; }
        /// <summary>
        /// 是否共享
        /// </summary>
        public bool IsShared { get; set; }
        /// <summary>
        /// 存储桶描述
        /// </summary>
        [StringAtrribute(200)]
        public string Description { get; set; }
        /// <summary>
        /// 图片id
        /// </summary>
        [StringAtrribute(200)]
        public string Conver { get; set; }
        /// <summary>
        /// 存储桶名称
        /// </summary>
        [StringAtrribute(50)]//字符串特性解决中文乱码问题（nvchar）
        public string Nsme { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(Resource.Id))]//BookA表中的studenId
        public List<Resource> Resources { get; set; }
    }
}
