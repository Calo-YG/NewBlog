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
        public static Type[] Increments = new Type[] { typeof(int), typeof(long) };
        public static ConfigureExternalServices AddContextColumsConfigure()
        {
            ConfigureExternalServices configure = new ConfigureExternalServices();

            configure.EntityService = (prop, column) =>
            {
                var attributes = prop.GetCustomAttributes();

                if (attributes.OfType<KeyWithIncrementAttribute>().Any() && attributes.Any(p => Increments.Contains(p.GetType())))
                {
                    column.IsPrimarykey = true;
                    column.IsIdentity = true;
                }

                //并发冲突
                if (attributes.OfType<ConcurrentTokenAttribute>().Any())
                {
                    column.IsEnableUpdateVersionValidation = true;
                }
            };

            configure.EntityNameService = (type, entity) =>
            {
                var attributes = type.GetCustomAttributes(true);

                if (attributes.Any(p => p is TableAttribute))
                {
                    entity.DbTableName = (attributes.FirstOrDefault(p => p is TableAttribute) as TableAttribute)?.Name ?? nameof(type);
                }
            };
            return configure;
        }
    }
}
