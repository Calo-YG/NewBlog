namespace Y.Blog.Host.Data
{
    public class LocalPhotoService
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger _logger;   
        public LocalPhotoService(IHostEnvironment hostEnvironment
            ,ILoggerFactory loggerFactory) 
        { 
            _hostEnvironment = hostEnvironment; 
            _logger = loggerFactory.CreateLogger<LocalPhotoService>();
        }

        public List<string> Photos()
        {
            List<string> afterfix = new List<string>()
            {
                "jpg",
                "jpeg"
            };

            var photos = new List<string>();    

            var dictory = Path.Join(_hostEnvironment.ContentRootPath, "wwwroot", "Photos");

            var prefix = "/Photos/";

            DirectoryInfo directoryInfo= new DirectoryInfo(dictory);

            if(!directoryInfo.Exists )
            {
                return photos;
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                var filename = file.Name;

                var fixs = filename.Split('.');

                if (!afterfix.Contains(fixs[fixs.Length - 1]))
                {
                    continue;
                }

                var photo = prefix + filename;

                photos.Add(photo);

            }
            _logger.LogInformation("静态资源图片数量");
            return photos;
        }
    }
}
