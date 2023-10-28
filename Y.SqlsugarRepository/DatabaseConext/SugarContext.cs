using SqlSugar;
namespace Y.SqlsugarRepository.DatabaseConext
{
    public  class SugarContext:SugarUnitOfWork
    {
        public virtual ISugarQueryable<TEntity> DbSet<TEntity>() where TEntity : class,new()
        {
            if (Db is null)
            {
                throw new ArgumentNullException("请将ISqlsugarClient注入到IOC容器中");
            }
            return Db.Queryable<TEntity>();
        }

    }

}
