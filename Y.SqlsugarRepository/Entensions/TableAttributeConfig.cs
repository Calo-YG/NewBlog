using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

                if (attributes.OfType<PrimaryKeyAttribute>().Any())
                {
                    column.IsPrimarykey = true;
                    column.IsNullable = false;
                }

                if (attributes.OfType<KeyWithIncrementAttribute>().Any())
                {
                    column.IsPrimarykey = true;
                    column.IsIdentity = true;
                    column.IsNullable = false;
                }

                //自动设置可空属性
               // if(new NullabilityInfoContext()
               //.Create(prop).WriteState is NullabilityState.Nullable)
               // {
               //     column.IsNullable = true;
               // }

                //并发冲突
                if (attributes.OfType<ConcurrentTokenAttribute>().Any())
                {
                    column.IsEnableUpdateVersionValidation = true;
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
