using FreeInterface.CommonModel;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace FreeInterface
{
    public interface ICommonHttpApi : IHttpApi
    {
        public Task<Sentence> GetSentence();
    }
}
