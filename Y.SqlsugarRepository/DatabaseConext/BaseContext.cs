using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.DatabaseConext
{
    public class BaseContext : SqlSugarClient
    {
        private ConnectionConfigOptions Options { get; set; }
        public BaseContext(ConnectionConfigOptions options) : base(options)
        {
            Options = options ?? throw new ArgumentException("数据库上下文注册optionsw为空");
            ConfigureFilter();
        }

        /// <summary>
        /// 配置查询过滤器
        /// </summary>
        public virtual void ConfigureFilter()
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
                this.QueryFilter.AddTableFilter<object>(expression);
            });
        }

        /// <summary>
        /// 配置Aop
        /// </summary>
        public virtual void ConfigureAop() { }
        /// <summary>
        /// 数据过滤器
        /// </summary>
        public virtual void ConfigureDataeFilter() { }
    }
}
