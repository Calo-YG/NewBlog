using Calo.Blog.Extenions.AppModule;

namespace Calo.Blog.Host
{
    public class CaloBlogHostModule : YModule
    {
        public override void ServiceConfiguration(IServiceConfigurationContext context)
        {
            var configuration = context.Configuration();
        }
    }
}
