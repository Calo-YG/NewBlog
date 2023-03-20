using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Extenions.AjaxResponse
{
    [Serializable]
    public class AjaxResponse<TResult> :AjaxResponseBase
    {
        public TResult? Result { get; set; } 

        public AjaxResponse(TResult result)
        {
            Result = result;
            Success= true;
        }

        public AjaxResponse() 
        { 
            Success= true;
        }

        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            Error = error;
            UnAuthorizedRequest = unAuthorizedRequest;
            Success = false;
        }

    }
    [Serializable]
    public class AjaxResponse : AjaxResponse<object>
    {
        public AjaxResponse(object result) : base(result)
        {
        }

        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest) : base(error, unAuthorizedRequest) 
        {

        }

        public AjaxResponse(object result, bool _unAuthorizedRequest):base(result)
        {
            UnAuthorizedRequest = _unAuthorizedRequest;
        }


        public AjaxResponse() : base(){ }
    }
}
