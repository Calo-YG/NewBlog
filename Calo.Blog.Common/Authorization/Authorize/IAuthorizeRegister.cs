namespace Calo.Blog.Common.Authorization.Authorize
{
    public interface IAuthorizeRegister
    {
        List<IAuthorizePermissionProvider> AuthorizeProviders {  get;}
    }
}
