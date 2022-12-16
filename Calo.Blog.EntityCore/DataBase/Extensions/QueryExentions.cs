using Calo.Blog.Extenions.InputAndOutDto;
using Calo.Blog.Extenions.InputEntity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DataBase.Extensions
{
    public static class QueryExentions
    {
        public static ISugarQueryable<TEntity> PageBy<TEntity>(this ISugarQueryable<TEntity> queryable,PageInput input)
        {
            return queryable.Take(input.MaxCount).Skip(input.SkipCount);
        }

        public static async Task<PageResultDto<TEntity>> PageListAsync<TEntity>(ISugarQueryable<TEntity> queryable , PageInput input)
        {
            RefAsync<int> TotalCount = 0;
            var items =await queryable.ToPageListAsync(input.MaxCount, input.SkipCount, TotalCount);
            return new PageResultDto<TEntity>() { 
            TotalCount=TotalCount,
            Items= items
            };
        }

        public static ISugarQueryable<TEntity> PagedBy<TEntity>(ISugarQueryable<TEntity> queryable,PageInput input)
        {
            return queryable.Take(input.MaxCount).Skip(input.SkipCount);
        }
    }
}
