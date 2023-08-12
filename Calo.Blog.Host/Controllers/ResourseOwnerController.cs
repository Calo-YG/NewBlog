using Calo.Blog.Application.Interfaces;
using Calo.Blog.Application.ResourceOwnereServices.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Calo.Blog.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResourseOwnerController: ControllerBase
    {
        private readonly IResourceOwnerApplicationService _resourceOwnerApplicationService;
        public ResourseOwnerController(IResourceOwnerApplicationService resourceOwnerApplicationService)
        {
            _resourceOwnerApplicationService = resourceOwnerApplicationService;
        }
        [HttpPost]
        public Task Create(CreateOwnerInput input)
        {
            return _resourceOwnerApplicationService.Create(input);
        }
        [HttpPatch]
        public Task Update(UpdateOwnerInput input)
        {
            return _resourceOwnerApplicationService.Update(input);
        }
    }
}
