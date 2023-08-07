using Microsoft.Extensions.DependencyInjection;
using Minio;
using MongoDB.Driver;

namespace Calo.Blog.Common.Minio
{
	public static class MinioExtensions
	{
		/// <summary>
		/// ssl 密钥创建minio链接
		/// </summary>
		/// <param name="services"></param>
		/// <param name="options"></param>
		public static async void AddMinio(this IServiceCollection services,Action<MinioConfig> options)
		{
			var config = new MinioConfig();

			options.Invoke(config);

		    var client = new MinioClient()
				.WithEndpoint(config.Host)
				.WithCredentials(config.AccessKey,config.SecretKey)
				.WithSSL()
				.Build();

			services.AddSingleton<MinioClient>(client);

            var defaultBucket = config.DefaultBucket;
            ///创建默认存储桶
            if (string.IsNullOrEmpty(config.DefaultBucket))
            {
				return;
            }
            var bucketArgs = new BucketExistsArgs();
            bucketArgs.WithBucket(config.DefaultBucket);

            if (await client.BucketExistsAsync(bucketArgs))
            {
				return;
            }

            var makeArgs = new MakeBucketArgs();
            makeArgs.WithBucket(config.DefaultBucket);

            await client.MakeBucketAsync(makeArgs);
        }
	}
}
