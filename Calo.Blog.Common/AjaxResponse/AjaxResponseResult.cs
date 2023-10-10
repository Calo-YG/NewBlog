using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AjaxResponse
{
    [Serializable]
    public class AjaxResponseResult<TResult> :AjaxResponseBase
    {
        public TResult? Result { get; set; } 

        public AjaxResponseResult(TResult result)
        {
            Result = result;
            Success= true;
        }

        public AjaxResponseResult() 
        { 
            Success= true;
        }

        public AjaxResponseResult(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }

    }
    [Serializable]
    public class AjaxResponseResult : AjaxResponseResult<object>
    {
        public AjaxResponseResult(object result) : base(result)
        {
        }

        public AjaxResponseResult(ErrorInfo error, bool unAuthorizedRequest) : base(error, unAuthorizedRequest) 
        {

        }

        public AjaxResponseResult(object result, bool _unAuthorizedRequest):base(result)
        {
            UnAuthorizedRequest = _unAuthorizedRequest;
        }


        public AjaxResponseResult() : base(){ }
    }
}
