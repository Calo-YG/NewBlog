using Calo.Blog.Application.Interfaces;
using Calo.Blog.Common.Authorization;
using Y.Module.DependencyInjection;

namespace Calo.Blog.Application.TokenAppService.Dtos
{
    public class TokenApplicationService : ITokenApplicationService, ITransientInjection
    {
        private readonly ITokenProvider _tokenprovider;

        public TokenApplicationService(ITokenProvider tokenProvider)
        {
            _tokenprovider= tokenProvider;
        }
        public (string Token,string RefreshToken) GenerateToken(Guid userId)
        {
            //throw new NotImplementedException();

            var model = new UserTokenModel();

            return _tokenprovider.GenerateToken(model);
        }
    }
}
