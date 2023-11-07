namespace Calo.Blog.Common.Authorization.Authorize
{
    public interface IAuthorizePermissionContext: IDisposable
    {
        SystemPermission DefinePermission { get; }
    }
}
