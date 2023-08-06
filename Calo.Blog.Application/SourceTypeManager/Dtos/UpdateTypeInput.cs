using Y.SqlsugarRepository.Dtos;

namespace Calo.Blog.Application.SourceTypeManager.Dtos
{
    public class UpdateTypeInput:InputBase<Guid>
    {
        public string Name { get; set; }    

        public string Description { get; set; } 
    }
}
