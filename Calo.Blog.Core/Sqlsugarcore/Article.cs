using Calo.Blog.Domain.Shared;
using Y.SqlsugarRepository.EntityAttribute;
using Y.SqlsugarRepository.EntityBase;

namespace Calo.Blog.Domain.Sqlsugarcore
{
    public class Article:Entity<Guid>
    {
        [PrimaryKey]
        public virtual Guid Id { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        [StringAtrribute(50)]
        public virtual string Title { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [StringAtrribute(50)]
        public virtual string Author { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringAtrribute(240)]
        public virtual string? Description { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        [StringAtrribute(100)]
        public virtual Guid? Cover { get; set; }
        /// <summary>
        /// 文章来源
        /// </summary>
        public virtual SourseType? SourseType { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public virtual string Content { get; set; } 
        /// <summary>
        /// 评论数量
        /// </summary>
        public virtual int Comment { get; set; }   
        /// <summary>
        /// 浏览
        /// </summary>
        public virtual int Browse { get; set; }
        /// <summary>
        /// 收藏
        /// </summary>
        public virtual int Collection { get; set; }
        /// <summary>
        /// 点赞
        /// </summary>
        public virtual int Like { get; set; }   


        public Article(Guid id , string title, string author, string? description, Guid cover,SourseType sourse,string content,int comment,int browse,int collection,int like)
        {
            Id= id;
            Title= title;
            Author= author;
            Description= description;
            Cover= cover;
            SourseType= sourse;
            Content= content;
            Comment= comment;
            Browse= browse;
            Collection= collection;
            Like= like;
        }
    }
}
