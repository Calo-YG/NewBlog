using Calo.Blog.Application.Interfaces;
using Calo.Blog.Application.SourceTypeManager.Dtos;
using Calo.Blog.Common.Excetptions;
using Calo.Blog.Domain.Sqlsugarcore;
using Y.Module.DependencyInjection;
using Y.SqlsugarRepository.Repository;

namespace Calo.Blog.Application.SourceTypeManager
{
    public class SourceTypeApplicationService:ISourceTypeApplicatonService,ITransientInjection
    {
        private readonly IBaseRepository<SourceType, Guid> _sourceTypeRepository;
        public SourceTypeApplicationService(IBaseRepository<SourceType, Guid> sourceTypeRepository) 
        { 
            _sourceTypeRepository = sourceTypeRepository;
        }
        public async Task Create (CreateTypeInput input)
        {
            var exists = await _sourceTypeRepository.AsQueryable().AnyAsync(p=>p.Name == input.Name);

            if (exists)
            {
                ThrowUserFriendlyException.ThrowException($"{input.Name}分类已存在");
            }

            SourceType type = new SourceType(input.Name,
                input.Description);

            await _sourceTypeRepository.InsertAsync(type);  
        }

        public async Task Update(UpdateTypeInput input)
        {
            SourceType sourceType = new SourceType(input.Id
                ,input.Name
                ,input.Description
                ,input.ConcurrentToken);

            await _sourceTypeRepository.UpdateAsync(sourceType);
        }
    }
}
