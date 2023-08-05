using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Y.Blog.Host.Models
{
    public class PhoneLogin
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required, DisplayName("手机号")]
        public string Phone { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required, DisplayName("验证码")]
        public string CheckCode { get; set; }
    }
}
