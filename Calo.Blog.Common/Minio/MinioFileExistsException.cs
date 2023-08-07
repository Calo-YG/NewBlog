using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.Minio
{
    public class MinioFileExistsException:Exception
    {
        public MinioFileExistsException() { }

        public MinioFileExistsException(string message) : base(message)
        {

        }
    }

    public static class ThrowMinioFileExistsException
    {
        public static void FileExistsException(string objectName)
        {
            throw new MinioFileExistsException($"{objectName}文件存在当前存储桶");
        }
    }

}
