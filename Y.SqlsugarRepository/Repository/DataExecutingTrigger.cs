using SqlSugar;

namespace Y.SqlsugarRepository.Repository
{
    public class DataExecutingTrigger
    {
        public string Property { get;private set; }

        public DataFilterType FilterType { get; private set; }

        public Func<object> Func { get; private set; }

        public DataExecutingTrigger(string property,DataFilterType filter, Func<object> func)
        {
            Property = property;
            FilterType = filter;
            Func = func;
        }
    }
}
