using Calo.Blog.Application.ResourceOwnereServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Application.Interfaces
{
    public interface IResourceOwnerApplicationService
    {
        Task Create(CreateOwnerInput input);

        Task Update(UpdateOwnerInput input);
    }
}
