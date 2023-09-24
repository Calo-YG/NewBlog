﻿using Microsoft.Extensions.Configuration;
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
		public static void AddMinio(this IServiceCollection services,IConfiguration configuration)
		{
            var config = configuration.GetSection("App:MinioConfig")
                          .Get<MinioConfig>();

			services.Configure<MinioConfig>(p =>
			{
                p.DefaultBucket = config.DefaultBucket;
                p.Protal = config.Protal;
                p.SecretKey = config.SecretKey;
                p.AccessKey = config.AccessKey;
                p.Host = config.Host;
                p.Password = config.Password;
                p.UserName = config.UserName;
            });

		    var client = new MinioClient()
				.WithEndpoint(config.Host)
				.WithCredentials(config.AccessKey,config.SecretKey)
				//取消ssl配置
				//.WithSSL()
				.Build();

			services.AddSingleton<MinioClient>(client);
        }
	}
}
