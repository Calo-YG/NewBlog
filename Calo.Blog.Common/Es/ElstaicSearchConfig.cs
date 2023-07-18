using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.ES
{
	public class ElstaicSearchConfig
	{
		/// <summary>
		/// es 服务地址
		/// </summary>
		public List<string> Urls { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }
	}
}
