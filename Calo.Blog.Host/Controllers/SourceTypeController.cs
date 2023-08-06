using Calo.Blog.Application.Interfaces;
using Calo.Blog.Application.SourceTypeManager.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Calo.Blog.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SourceTypeController: ControllerBase
    {
        private readonly ISourceTypeApplicatonService _sourceApplicationService;
        public SourceTypeController(ISourceTypeApplicatonService sourceTypeApplicatonService) 
        {
            _sourceApplicationService= sourceTypeApplicatonService;
        }

        [HttpPost]
        [Authorize]
        public Task Create(CreateTypeInput input)
        {
            return _sourceApplicationService.Create(input);
        }

        [HttpPatch]
        [Authorize]
        public Task Update(UpdateTypeInput input)
        {
            return _sourceApplicationService.Update(input);
        }
    }
}
