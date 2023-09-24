using Microsoft.Extensions.DependencyInjection;
using Y.Module;
using Y.Module.Extensions;
using Y.Module.Modules;

namespace Calo.Blog.Common.Minio
{
    public class MinioModule:YModule
    {
        public override void ConfigerService(ConfigerServiceContext context)
        {
            var configurarion = context.GetConfiguartion();

            context.Services.AddMinio(configurarion);

            context.Services.AddTransient<IMinioService, MinioService>();
        }

        public override async Task LaterInitApplicationAsync(InitApplicationContext context)
        {
            var scope = context.ServiceProvider.CreateAsyncScope();

            var minioService = scope.ServiceProvider.GetRequiredService<IMinioService>();

            //minio需要配置https
            await scope.ServiceProvider
                .GetRequiredService<IMinioService>()
                .CreateDefaultBucket();

            await Task.CompletedTask;
        }
    }
}
