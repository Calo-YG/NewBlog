namespace Calo.Blog.Common.Authorization.Authorize
{
    public abstract  class AuthorizePermissionProvider:IAuthorizePermissionProvider
    {
        public abstract void PermissionDefined(IAuthorizePermissionContext context);
    } 
}
