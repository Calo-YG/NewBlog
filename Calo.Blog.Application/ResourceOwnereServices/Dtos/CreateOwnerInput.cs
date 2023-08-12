using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.EntityAttribute;

namespace Calo.Blog.Application.ResourceOwnereServices.Dtos
{
    public class CreateOwnerInput
    {
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 是否启用密钥访问
        /// </summary>
        public bool Secrect { get; set; } = false;
    }
}
