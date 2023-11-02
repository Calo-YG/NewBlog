using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.DatabaseConext
{
    /// <summary>
    /// 数据库实体类型提供程序
    /// </summary>
    public class EntityProvider : IEntityProvider
    {
        public Type[] Entitys { get => _entitys.ToArray(); }

        public IServiceCollection Services { get; set; }

        private List<Type> _entitys { get; set; }

        static Type[] BaseTypes = new Type[] { typeof(IEntity<>),
            typeof(IEnity),
            typeof(IAggregateRoot<>),
            typeof (IAggregateRoot),
            typeof(IFullAggregateRoot<>),
            typeof(IFullAggregateRoot)
        };

        public EntityProvider(IServiceCollection services
            , List<Type> entitys)
        {
            _entitys = entitys;
            Services = services;
            services.AddSingleton<IEntityProvider>(this);
        }

        public EntityProvider(IServiceCollection services)
        {
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

        public IEntityProvider AddEntity(Type type)
        {
            CheckEntity(type);
            _entitys.Add(type);
            return this;
        }

        public IEntityProvider AddEntity<TEntity>()
        {
            CheckEntity<TEntity>();
            _entitys.Add(typeof(TEntity));
            return this;
        }

        public virtual void CheckEntity(Type type)
        {
            Check(type);
        }

        public virtual void CheckEntity<TEntity>()
        {
            Check(typeof(TEntity));
        }

        protected virtual void Check(Type type)
        {
            var isEntity = type.GetTypeInfo()
                .GetInterfaces()
                .Any(p => p.GetTypeInfo().IsGenericType && BaseTypes.Contains(p.GetGenericTypeDefinition()));
            if (!isEntity)
            {
                throw new ApplicationException("没有找到实体类型主键: " + type.Name + ". 确认实体是否继承了IEntity接口");
            }
        }
    }
}
