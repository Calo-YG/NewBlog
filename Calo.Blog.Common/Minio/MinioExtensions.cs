using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Calo.Blog.Common.Minio
{
	public static class MinioExtensions
	{
		/// <summary>
		/// ssl 密钥创建minio链接
		/// </summary>
		/// <param name="services"></param>
		/// <param name="options"></param>
		public static void AddMinio(this IServiceCollection services,Action<MinioConfig> options)
		{
			var config = new MinioConfig();

			options.Invoke(config);

		    var client = new MinioClient()
				.WithEndpoint(config.Host)
				.WithCredentials(config.AccessKey,config.SecretKey)
				.WithSSL()
				.Build();

			services.AddSingleton<MinioClient>(client);
        }
	}
}
