using Calo.Blog.Application.Interfaces;
using Calo.Blog.Domain.Sqlsugarcore;
using Y.Module.DependencyInjection;
using Y.SqlsugarRepository.Repository;

namespace Calo.Blog.Application.ResourceOwnereServices
{
    public class ResourceOwnerApplictionService:IResourceOwnerApplicationService,ITransientInjection
    {
        private readonly IBaseRepository<ResourceOwner, Guid> _resourOwnerRepository;

        public ResourceOwnerApplictionService(IBaseRepository<ResourceOwner,Guid> resourceOwnerRepository)
        {
            _resourOwnerRepository= resourceOwnerRepository;
        }


    }
}
