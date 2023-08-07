namespace Calo.Blog.Common.Minio
{
    public interface IMinioService
    {
        /// <summary>
        /// 创建存储桶
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        Task CreateBucketAsync(string bucketName);
        /// <summary>
        /// 删除存储桶
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        Task RemoveBucket(string bucketName);
        /// <summary>
        /// 获取存储桶存储对象数据流
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ObjectOutPut> GetObjectAsync(GetObjectInput input);
        /// <summary>
        /// 上传文件对象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UploadObjectAsync(UploadObjectInput input);
        /// <summary>
        /// 上传本地文件使用默认存储桶
        /// </summary>
        /// <param name="uploads"></param>
        /// <returns></returns>
        Task UploadLocalUseDefaultBucket(List<UploadObjectInput> uploads);
    }
}
