using Calo.Blog.Application.Interfaces;
using Calo.Blog.Application.ResourceOwnereServices.Dtos;
using Calo.Blog.Common.Excetptions;
using Calo.Blog.Domain.Sqlsugarcore;
using Y.Module.DependencyInjection;
using Y.SqlsugarRepository.Repository;

namespace Calo.Blog.Application.ResourceOwnereServices
{
    public class ResourceOwnerApplictionService:IResourceOwnerApplicationService,ITransientDependency
    {
        private readonly IBaseRepository<ResourceOwner, Guid> _resourOwnerRepository;

        public ResourceOwnerApplictionService(IBaseRepository<ResourceOwner,Guid> resourceOwnerRepository)
        {
            _resourOwnerRepository= resourceOwnerRepository;
        }
        /// <summary>
        /// 创建资源拥有者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Create(CreateOwnerInput input)
        {
            var isExists = await _resourOwnerRepository.AsQueryable().AnyAsync(p => p.Name == input.Name);

            if (!isExists)
            {
                ThrowUserFriendlyException.ThrowException($"{input.Name}资源拥有者已存在");
            }

            var owner = new ResourceOwner(
                input.Name
                ,input.PassWord
                ,input.Description
                , input.Secrect
                );

            await _resourOwnerRepository.InsertAsync(owner);
        }
        /// <summary>
        /// 更新资源拥有者信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Update(UpdateOwnerInput input)
        {
            var owner = new ResourceOwner(
                input.Id
                ,input.Name
                ,input.PassWord
                ,input.Description
                ,input.ConcurrentToken
                ,input.Secrect
                );

            await _resourOwnerRepository.UpdateAsync(owner);
        }
    }
}
