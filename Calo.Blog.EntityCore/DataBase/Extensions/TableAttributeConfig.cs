using Calo.Blog.EntityCore.DataBase.EntityAttribute;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Extensions
{
    public static class TableAttributeConfig
    {
        public static Type[] Increments =new Type[] { typeof(int),typeof(long) };
        public static ConfigureExternalServices AddContextColumsConfiure()
        {
            ConfigureExternalServices configure = new ConfigureExternalServices();

            configure.EntityService = (prop, column) =>
            {
                var attributes = prop.GetCustomAttributes(true);

                if(attributes.Any(p=>p is KeyWithIncrement) && attributes.Any(p=> Increments.Contains(p.GetType())))
                {
                    column.IsPrimarykey = true;
                    column.IsIdentity = true;
                }

                //并发冲突
                if(attributes.Any(p=>p is ConcurrentTokenAttribute))
                {
                    column.IsEnableUpdateVersionValidation = true;
                }
            };

            configure.EntityNameService = (type, entity) =>
            {
                var attributes = type.GetCustomAttributes(true);
                
                if(attributes.Any(p=>p is TableAttribute))
                {
                    entity.DbTableName = (attributes.FirstOrDefault(p => p is TableAttribute) as TableAttribute)?.Name ?? nameof(type);
                }
            };
            return configure;
        }
    }
}
