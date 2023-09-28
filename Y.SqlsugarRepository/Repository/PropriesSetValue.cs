using Microsoft.AspNetCore.Http;
using SqlSugar;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace Y.SqlsugarRepository.Repository
{
    public class PropriesSetValue : IPropriesSetValue
    {
        public ConcurrentBag<DataExecutingTrigger> DataExecutingTriggers {  get;private set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public PropriesSetValue(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            SetExecutingValue();
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
            DataExecutingTriggers.Add(new DataExecutingTrigger("IsDeleted", DataFilterType.InsertByObject, () => { return false; }));

            //更新操作
            DataExecutingTriggers.Add(new DataExecutingTrigger("UpdateTime", DataFilterType.UpdateByObject, () => { return DateTime.Now; }));
            DataExecutingTriggers.Add(new DataExecutingTrigger("UpdateUserId", DataFilterType.UpdateByObject, GetUserId));

            //删除操作
            DataExecutingTriggers.Add(new DataExecutingTrigger("DeleteTime", DataFilterType.DeleteByObject, () => { return DateTime.Now; }));
            DataExecutingTriggers.Add(new DataExecutingTrigger("DeleteUserId", DataFilterType.DeleteByObject, GetUserId));
            DataExecutingTriggers.Add(new DataExecutingTrigger("IsDeleted",DataFilterType.DeleteByObject, ()=>true));
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

    }
}
