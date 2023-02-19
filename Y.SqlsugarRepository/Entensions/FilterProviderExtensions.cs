using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Entensions
{
    public static class FilterProviderExtensions
    {
        public static void ConfigureFilterForEntity(this QueryFilterProvider provider)
        {

            Type[] types = new Type[]
            {
                typeof(Entity<>),
                typeof(AutiedEntity<>),
                typeof(FullAutiedEntity<>)
            };

            var entityTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(p => types.Contains(p.BaseType))
                .ToList();

            ParameterExpression paraExpression;
            MemberExpression fieldExpression;
            BinaryExpression binaryExpression;
            Expression<Func<Object, bool>> expression;

            entityTypes.ForEach(p =>
            {
                paraExpression = Expression.Parameter(p, "p");
                fieldExpression = Expression.Property(paraExpression, "IsDelete");
                binaryExpression = Expression.Equal(fieldExpression, Expression.Constant(true));
                expression = Expression.Lambda<Func<object, bool>>(binaryExpression, paraExpression);
                provider.AddTableFilter<object>(expression);
            });
        }
    }
}
