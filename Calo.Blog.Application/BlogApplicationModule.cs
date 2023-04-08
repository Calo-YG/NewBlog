using Calo.Blog.Common.Authorization.Authorize;
using Calo.Blog.Domain;
using Microsoft.Extensions.DependencyInjection;
using Y.Module;
using Y.Module.Modules;

namespace Calo.Blog.Application
{
    [DependOn(typeof(BlogCoreModule))]
    public class BlogApplicationModule:YModule
    {
        public override void LaterInitApplication(InitApplicationContext context)
        {
            var scope = context.ServiceProvider.CreateScope();
            var authorizeManager = scope.ServiceProvider.GetRequiredService<IAuthorizeManager>();
            authorizeManager.AddAuthorizeRegiester();
        }
    }
}
