using Y.SqlsugarRepository.Dtos;

namespace Calo.Blog.Application.ResourceOwnereServices.Dtos
{
    public class UpdateOwnerInput : InputBase<Guid>
    {
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 是否启用密钥访问
        /// </summary>
        public bool Secrect { get; set; }
    }
}
