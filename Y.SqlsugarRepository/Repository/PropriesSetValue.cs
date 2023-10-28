using Microsoft.AspNetCore.Http;
using SqlSugar;
using System.Collections.Concurrent;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Security.Claims;
using Y.SqlsugarRepository.DatabaseConext;

namespace Y.SqlsugarRepository.Repository
{
    public class PropriesSetValue : IPropriesSetValue,IDisposable
    {
        public ConcurrentBag<DataExecutingTrigger> DataExecutingTriggers {  get;private set; }

        public ConcurrentBag<EntityFilter> Filters { get;private set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IEntityContainer _entityContainer;
        
        public PropriesSetValue(IHttpContextAccessor httpContextAccessor
            , IEntityContainer entityContainer)
        {
            _httpContextAccessor = httpContextAccessor;
            _entityContainer = entityContainer;
            SetExecutingValue();
            InitFilter();   
        }

        public virtual void SetExecutingValue()
        {
            if (DataExecutingTriggers is not null)
            {
                return;
            }
            DataExecutingTriggers = new ConcurrentBag<DataExecutingTrigger>();

            //插入操作
            DataExecutingTriggers.Add(new DataExecutingTrigger("CreationTime", DataFilterType.InsertByObject, () => { return DateTime.Now; }));
            DataExecutingTriggers.Add(new DataExecutingTrigger("CreatorUserId", DataFilterType.InsertByObject, GetUserId));
            DataExecutingTriggers.Add(new DataExecutingTrigger("CreatorUserName", DataFilterType.InsertByObject, GetUserName));
            DataExecutingTriggers.Add(new DataExecutingTrigger("IsDeleted", DataFilterType.InsertByObject, () => false));
            DataExecutingTriggers.Add(new DataExecutingTrigger("ConcurrentToken", DataFilterType.InsertByObject, () => "7e85e989-3d84-43c4-8cc2-32f89c99174b"));

            //更新操作
            DataExecutingTriggers.Add(new DataExecutingTrigger("UpdateTime", DataFilterType.UpdateByObject, () => { return DateTime.Now; }));
            DataExecutingTriggers.Add(new DataExecutingTrigger("UpdateUserId", DataFilterType.UpdateByObject, GetUserId));

            ////删除操作
            //DataExecutingTriggers.Add(new DataExecutingTrigger("DeleteTime", DataFilterType.DeleteByObject, () => { return DateTime.Now; }));
            //DataExecutingTriggers.Add(new DataExecutingTrigger("DeleteUserId", DataFilterType.DeleteByObject, GetUserId));
            //DataExecutingTriggers.Add(new DataExecutingTrigger("IsDeleted",DataFilterType.DeleteByObject, ()=>true));
        }

        public virtual void InitFilter()
        {
            if(Filters is not null)
            {
                return;
            }

            Filters = new ConcurrentBag<EntityFilter>();

            var entityTypes = _entityContainer.EntityTypes;

            EntityFilter filter = null;

            foreach (var type in entityTypes)
            {
                if (!type.GetProperties().Any(p => p.Name.Equals("IsDeleted")))
                {
                    continue;
                }

                var lambda = DynamicExpressionParser.ParseLambda
                                         (new[] { Expression.Parameter(type, "p") },
                                          typeof(bool), $"IsDeleted ==  @0",
                                           false);

                filter=new EntityFilter(type, lambda);

                Filters.Add(filter);
            }
        }

        public virtual string GetUserId()
        {
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            return claims?.FirstOrDefault(p => p.Type == "Id")?.Value ?? "";
        }

        public virtual string GetUserName()
        {
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            return claims?.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value ?? ""; ;
        }

        public void Dispose()
        {
            if(Filters != null)
            {
                Filters.Clear();
                Filters = null;
            }
            if(DataExecutingTriggers != null)
            {
                DataExecutingTriggers.Clear();
                DataExecutingTriggers = null;
            }
        }
    }
}
