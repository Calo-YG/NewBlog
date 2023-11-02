using Calo.Blog.Common.Authorization.Authorize;

namespace Calo.Blog.Common.Authorization
{
    public interface IAuthorizePermissionProvider
    {
        void PermissionDefined(IAuthorizePermissionContext context);
    }
}
