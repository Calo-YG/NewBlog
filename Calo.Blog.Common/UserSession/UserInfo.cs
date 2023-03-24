using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.UserSession
{
    public class UserInfo
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string? RoleName { get; set; }
        /// <summary>
        /// 用户角色id
        /// </summary>
        public long[]? RoleIds { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string? UserEmail { get; set; }
    }
}
