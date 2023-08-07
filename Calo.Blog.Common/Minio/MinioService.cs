using Minio;
using Y.Module.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio.DataModel;
using System.Security.Cryptography;

namespace Calo.Blog.Common.Minio
{
    public class MinioService:IMinioService,IScopedInjection
    {
        private readonly MinioClient _minioClient;

        private readonly IOptions<MinioConfig> _minioOptions;
        public MinioService(MinioClient minioClient
            , IOptions<MinioConfig> minioOptions)
        {
            _minioClient = minioClient;
            _minioOptions = minioOptions;
        }

        /// <summary>
        /// 当存储桶为空时使用默认存储桶
        /// </summary>
        /// <param name="bucketName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void SetDefaultPrimaryKey(string bucketName)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                bucketName = _minioOptions.Value.DefaultBucket ?? throw new ArgumentNullException("Minio基础配置默认存储桶为空");
            }
        }
        /// <summary>
        /// 创建存储桶
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task CreateBucketAsync(string bucketName)
        {
            ///设置存储桶
            SetDefaultPrimaryKey(bucketName);

            var bucketArgs = new BucketExistsArgs();
            bucketArgs.WithBucket(bucketName);

            if (!await _minioClient.BucketExistsAsync(bucketArgs))
            {
                ThrowBucketNotExistisException.ExistsException(bucketName);
            }

            var makeArgs = new MakeBucketArgs();
            makeArgs.WithBucket(bucketName);

            await _minioClient.MakeBucketAsync(makeArgs);
        }
        /// <summary>
        /// 删除存储桶
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task RemoveBucket(string bucketName)
        {
            var bucketArgs = new BucketExistsArgs();
            bucketArgs.WithBucket(bucketName);

            if (!await _minioClient.BucketExistsAsync(bucketArgs))
            {
                ThrowBucketNotExistisException.NotExistsException(bucketName);
            }

            var removeArgs = new RemoveBucketArgs();
            removeArgs.WithBucket(bucketName);

            await _minioClient.RemoveBucketAsync(removeArgs);
        }
        /// <summary>
        /// 获取存储桶存储对象数据流
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ObjectOutPut> GetObjectAsync(GetObjectInput input)
        {
            StatObjectArgs statObjectArgs = new StatObjectArgs()
                                    .WithBucket(input.BucketName)
                                    .WithObject(input.ObjectName);

            await _minioClient.StatObjectAsync(statObjectArgs);

            Stream objStream = null;

            GetObjectArgs getObjectArgs = new GetObjectArgs()
                      .WithBucket(input.BucketName)
                      .WithObject(input.ObjectName)
                      .WithCallbackStream((stream) =>
                      {
                          //stream.CopyTo(Console.OpenStandardOutput());
                          objStream=stream ?? throw new ArgumentNullException("Minio文件对象流为空");
                      });

            var statObj=  await _minioClient.GetObjectAsync(getObjectArgs);

            return new ObjectOutPut(statObj.ObjectName
                ,objStream 
                ,statObj.ContentType);
        }
        /// <summary>
        /// 上传文件对象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UploadObjectAsync(UploadObjectInput input)
        {
            Aes aesEncryption = Aes.Create();
            aesEncryption.KeySize = 256;
            aesEncryption.GenerateKey();

            var ssec = new SSEC(aesEncryption.Key);

            PutObjectArgs putObjectArgs = new PutObjectArgs()
                                              .WithBucket(input.BucketName)
                                              .WithObject(input.ObjectName)
                                              .WithStreamData(input.Stream)
                                              .WithContentType(input.ContentType)
                                              .WithServerSideEncryption(ssec);

            await _minioClient.PutObjectAsync(putObjectArgs);
        }
        /// <summary>
        /// 上传本地文件使用默认存储桶
        /// </summary>
        /// <param name="uploads"></param>
        /// <returns></returns>
        public async  Task UploadLocalUseDefaultBucket(List<UploadObjectInput> uploads)
        {
            List<Task> tasks= new List<Task>();

            foreach(var item in uploads)
            {
                var task = UploadObjectAsync(item);

                if(tasks.Count > 25)
                {
                    await Task.WhenAll(tasks);

                    tasks.Clear();
                }
            }
        }
    }
}
