namespace Calo.Blog.Common.Minio
{
    public class UploadObjectInput:IDisposable
    {
        public string BucketName { get; set; }

        public string ObjectName { get; set; }

        public string ContentType { get; set; } 

        public Stream Stream { get; set; }

        private bool Disposed { get; set; }=false;

        public UploadObjectInput() { }

        public UploadObjectInput(string bucketName,string objectName,string contentType,Stream stream)
        {
            BucketName= bucketName;
            ObjectName= objectName;
            ContentType= contentType;
            Stream = stream;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (Disposed)
            {
                return;
            }
            if (Stream != null)
            {
                Stream.Close();
                Stream.Dispose();  
            }
            Disposed = true;
        }
    }
}
