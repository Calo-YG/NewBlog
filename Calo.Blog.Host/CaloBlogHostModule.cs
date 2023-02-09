using Calo.Blog.Extenions.AppModule;

namespace Calo.Blog.Host
{
    public class CaloBlogHostModule : YModule
    {
        public override void ServiceConfiguration(ServiceConfigurationContext context)
        {
            var configuration = context.Configuration();
        }
    }
}
