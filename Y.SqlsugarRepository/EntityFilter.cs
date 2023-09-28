using System.Linq.Expressions;

namespace Y.SqlsugarRepository
{
    public class EntityFilter
    {
        public Type EntityType { get;private set; }

        public LambdaExpression LambdaExpression { get; private set; }

        public EntityFilter(Type entityType, LambdaExpression lambdaExpression)
        {
            EntityType = entityType;
            LambdaExpression = lambdaExpression;
        }   
    }
}
