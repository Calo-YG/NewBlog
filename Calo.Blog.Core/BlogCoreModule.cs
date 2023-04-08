using Calo.Blog.Common;
using Calo.Blog.EntityCore;
using Y.Module;
using Y.Module.Modules;

namespace Calo.Blog.Domain
{
    [DependOn(typeof(SqlSugarEnityCoreModule), typeof(CommonModule))]
    public class BlogCoreModule:YModule
    {
    }
}
