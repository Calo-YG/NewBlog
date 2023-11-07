using Microsoft.Extensions.DependencyInjection;
namespace Y.SqlsugarRepository.DatabaseConext
{
    public interface IEntityProvider:IDisposable
    {
        /// <summary>
        /// 数据库实体
        /// </summary>
        public Type[] Entitys { get; }

        public IServiceCollection Services { get; set; }
        /// <summary>
        /// 添加数据库实体类型
        /// </summary>
        /// <param name="type"></param>
        void Add(Type type);
        /// <summary>
        /// 添加数据库实体类型
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        void Add<TEntity>();
        /// <summary>
        /// 添加数据库实体类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEntityProvider AddEntity(Type type);
        /// <summary>
        /// 添加数据库实体类型
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IEntityProvider AddEntity<TEntity>();
    }
}
