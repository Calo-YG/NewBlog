using Calo.Blog.Application.SourceTypeManager.Dtos;
using Y.Module.DependencyInjection;

namespace Calo.Blog.Application.Interfaces
{
    public interface ISourceTypeApplicatonService
    {
        Task Create(CreateTypeInput input);

        Task Update(UpdateTypeInput input);
    }
}
