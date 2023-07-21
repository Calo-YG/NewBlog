namespace Calo.Blog.Common.Minio
{
	public class MinioConfig
	{
		/// <summary>
		/// minio服务器地址
		/// </summary>
		public string Host { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string AccessKey { get; set; }	
		/// <summary>
		/// 密码
		/// </summary>
		public string SecretKey { get; set; }
		/// <summary>
		/// 默认存储桶名称
		/// </summary>
		public string DefaultBucket { get; set; }
		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }	
		/// <summary>
		/// 操作面板
		/// </summary>
		public string Protal { get; set; }
	}
}
