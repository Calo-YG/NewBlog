using SqlSugar;
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

        [Navigate(NavigateType.OneToMany, nameof(Resource.SourceTypeId))]//BookA表中的studenId
        public List<Resource> Resources { get; set; }

        public SourceType() { }

        public SourceType(string name,string? description)
        {
            Name= name; 
            Description= description ?? "暂无描述";
        }

        public SourceType(Guid id,string name,string description,string concurrentToken)
        {
            Id= id;
            Name= name;
            Description = description ?? "暂无描述";
            ConcurrentToken = concurrentToken;
        }
    }
}
