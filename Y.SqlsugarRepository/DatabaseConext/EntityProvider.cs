using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.DatabaseConext
{
    /// <summary>
    /// 数据库实体类型提供类
    /// </summary>
    public class EntityProvider : IEntityProvider
    {
        public Type[] Entitys { get => _entitys.ToArray(); }

        public IServiceCollection Services { get; set; }

        private List<Type> _entitys { get; set; }

        public EntityProvider(IServiceCollection services
            , List<Type> entitys)
        {
            _entitys = entitys;
            Services = services;
            _entitys = new List<Type>();
            services.AddSingleton<IEntityProvider>(this);
        }

        public void Add(Type type)
        {
            CheckEntity(type);
            _entitys.Add(type);
        }

        public void Add<TEntity>()
        {
            CheckEntity<TEntity>();
            _entitys.Add(typeof(TEntity));
        }

        public IEntityProvider AddEnity(Type type)
        {
            CheckEntity(type);
            _entitys.Add(type);
            return this;
        }

        public IEntityProvider AddEnity<TEntity>()
        {
            CheckEntity<TEntity>();
            _entitys.Add(typeof(TEntity));
            return this;
        }

        private void CheckEntity(Type type) { }

        private void CheckEntity<TEntity>() { }
    }
}
