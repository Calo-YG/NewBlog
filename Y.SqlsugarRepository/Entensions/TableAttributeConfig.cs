using SqlSugar;
using System.Reflection;
using Y.SqlsugarRepository.EntityAttribute;

namespace Y.SqlsugarRepository.Entensions
{
    public static class TableAttributeConfig
    {
        public static ConfigureExternalServices AddContextColumsConfigure()
        {
            ConfigureExternalServices configure = new ConfigureExternalServices();

            configure.EntityService = (prop, column) =>
            {
                var attributes = prop.GetCustomAttributes();

                //主键特性
                if (attributes.OfType<PrimaryKeyAttribute>().Any())
                {
                    column.IsPrimarykey = true;
                    column.IsNullable = false;
                }
                //主键自增特性
                if (attributes.OfType<KeyWithIncrementAttribute>().Any())
                {
                    column.IsPrimarykey = true;
                    column.IsIdentity = true;
                    column.IsNullable = false;
                }

                //解决中文乱码问题
                if(attributes.OfType<StringAtrribute>().Any() && prop.PropertyType== typeof(string))
                {
                    var strAttribute = attributes.OfType<StringAtrribute>().FirstOrDefault();
                    column.DataType = strAttribute._Type;
                    column.Length = strAttribute._length;
                }

                //自动设置可空属性
                if (new NullabilityInfoContext()
               .Create(prop).WriteState is NullabilityState.Nullable)
                {
                    column.IsNullable = true;
                }

                //并发冲突
                if (attributes.OfType<ConcurrentTokenAttribute>().Any())
                {
                    var tokenAttribute = attributes.OfType<ConcurrentTokenAttribute>().FirstOrDefault();
                    
                    column.IsEnableUpdateVersionValidation = tokenAttribute.Enabled;
                }
            };

            configure.EntityNameService = (type, entity) =>
            {
                var attributes = type.GetCustomAttributes();

                if (attributes.Any(p => p is TableAttribute))
                {
                    entity.DbTableName = (attributes.FirstOrDefault(p => p is TableAttribute) as TableAttribute)?.Name ?? nameof(type);
                }
            };
            return configure;
        }
    }
}
