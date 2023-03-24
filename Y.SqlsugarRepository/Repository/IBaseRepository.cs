using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.EntityBase;

namespace Y.SqlsugarRepository.Repository
{
    public interface IBaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity>
        where TEntity : class, IEntity<TPrimaryKey>
    {
    }

    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        ISugarQueryable<TEntity> AsQueryable();

        List<TEntity> GetList(Expression<Func<TEntity, bool>> whereExpression);

        TEntity GetSingle(Expression<Func<TEntity, bool>> whereExpression);

        TEntity GetFirst(Expression<Func<TEntity, bool>> whereExpression);

        bool IsAny(Expression<Func<TEntity, bool>> whereExpression);

        int Count(Expression<Func<TEntity, bool>> whereExpression);

        List<TEntity> GetPageList(Expression<Func<TEntity, bool>> whereExpression, PageModel page);

        List<TEntity> GetPageList(Expression<Func<TEntity, bool>> whereExpression, PageModel page, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        List<TEntity> GetPageList(List<IConditionalModel> conditionalList, PageModel page);

        List<TEntity> GetPageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        bool Insert(TEntity insertObj);

        bool InsertOrUpdate(TEntity data);

        bool InsertOrUpdate(List<TEntity> datas);

        int InsertReturnIdentity(TEntity insertObj);

        long InsertReturnBigIdentity(TEntity insertObj);

        long InsertReturnSnowflakeId(TEntity insertObj);

        List<long> InsertReturnSnowflakeId(List<TEntity> insertObjs);

        Task<long> InsertReturnSnowflakeIdAsync(TEntity insertObj);

        Task<List<long>> InsertReturnSnowflakeIdAsync(List<TEntity> insertObjs);

        TEntity InsertReturnEntity(TEntity insertObj);

        bool InsertRange(TEntity[] insertObjs);

        bool InsertRange(List<TEntity> insertObjs);

        bool Update(TEntity updateObj);
        bool UpdateRange(TEntity[] updateObjs);

        bool UpdateRange(List<TEntity> updateObjs);

        bool Update(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression);

        bool UpdateSetColumnsTrue(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression);

        bool Delete(TEntity deleteObj);

        bool Delete(List<TEntity> deleteObjs);

        bool Delete(Expression<Func<TEntity, bool>> whereExpression);

        Task<List<TEntity>> GetListAsync();

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task<List<TEntity>> GetPageListAsync(Expression<Func<TEntity, bool>> whereExpression, PageModel page);

        Task<List<TEntity>> GetPageListAsync(Expression<Func<TEntity, bool>> whereExpression, PageModel page, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        Task<List<TEntity>> GetPageListAsync(List<IConditionalModel> conditionalList, PageModel page);

        Task<List<TEntity>> GetPageListAsync(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<TEntity, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        Task<bool> IsAnyAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task<bool> InsertOrUpdateAsync(TEntity data);

        Task<bool> InsertOrUpdateAsync(List<TEntity> datas);

        Task<bool> InsertAsync(TEntity insertObj);

        Task<int> InsertReturnIdentityAsync(TEntity insertObj);

        Task<long> InsertReturnBigIdentityAsync(TEntity insertObj);

        Task<TEntity> InsertReturnEntityAsync(TEntity insertObj);

        Task<bool> InsertRangeAsync(TEntity[] insertObjs);

        Task<bool> InsertRangeAsync(List<TEntity> insertObjs);

        Task<bool> UpdateAsync(TEntity updateObj);

        Task<bool> UpdateRangeAsync(TEntity[] updateObjs);

        Task<bool> UpdateRangeAsync(List<TEntity> updateObjs);

        Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression);

        Task<bool> UpdateSetColumnsTrueAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression);

        Task<bool> DeleteAsync(TEntity deleteObj);

        Task<bool> DeleteAsync(List<TEntity> deleteObjs);

        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> whereExpression);

        Task<TEntity> InsertReturnEnityAsync(TEntity entity);

        Task BatchInsertAsync(List<TEntity> entitys);

        Task BatchFastInsertAsync(List<TEntity> entities, int pageSize = 1000);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null);

        bool Any(Expression<Func<TEntity, bool>>? expression = null);

        Task BatchUpdateAsync(List<TEntity> entityes);

        Task FastBatchUpdateAsync(List<TEntity> entities);

        Task UpdateColumnsAsync(TEntity entity, Expression<Func<TEntity, object>>? columns = null);

        Task UpdateColumnsAsync(TEntity entity, params string[]? columns);

        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>>? expression = null);

        Task DeleteAsync(TEntity entity, string? logic = null);
    }
}
