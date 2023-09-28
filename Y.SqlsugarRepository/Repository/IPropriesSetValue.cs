using System.Collections.Concurrent;

namespace Y.SqlsugarRepository.Repository
{
    public interface IPropriesSetValue
    {
        ConcurrentBag<DataExecutingTrigger> DataExecutingTriggers { get; }

        ConcurrentBag<EntityFilter> Filters { get;}
    }
}
