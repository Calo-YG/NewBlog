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
    public class EntityProvider
    {
        public Type[] Entitys { get => _entitys.ToArray(); }

        private List<Type> _entitys { get; set; }

        public EntityProvider(List<Type> entitys)
        {
            _entitys = entitys;
        }

        static EntityProvider() { }

        public void Add(Type type)
        {
            _entitys.Add(type);
        }

        public void Add<TEntity>()
        {
            _entitys.Add(typeof(TEntity));
        }
    }
}
