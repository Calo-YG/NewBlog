namespace Calo.Blog.Common.Authorization.Authorize
{
    public abstract  class AuthorizePermissionProvider
    {
        public abstract void PermissionDefined(IAuthorizePermissionContext context);
    } 
}
