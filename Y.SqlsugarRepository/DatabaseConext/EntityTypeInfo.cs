using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public class EntityTypeInfo
    {
        /// <summary>
        /// 数据库实体类型
        /// </summary>
        public Type EntityType { get; private set; }
        /// <summary>
        /// 数据库上下文包含Dbset
        /// </summary>
        public Type DeclaringType { get; private set; }

        public EntityTypeInfo(Type entityType, Type declaringType)
        {
            EntityType = entityType;
            DeclaringType = declaringType;
        }
    }
}
