namespace Calo.Blog.Common.Authorization.Authorize
{
    public interface IAuthorizePermissionContext
    {
        SystemPermission DefinePermission { get; }
    }
}
