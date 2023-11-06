using Calo.Blog.Common.Authorization.Authorize;

namespace Calo.Blog.Application.RegiesterPermissions
{
    public class TestAuthorizePermissionProvider : AuthorizePermissionProvider
    {
        public override void PermissionDefined(IAuthorizePermissionContext context)
        {
          
           var grouppermission= context.DefinePermission.AddGroup("首页", "Index");

            var childern = grouppermission.AddChild("组织单元", "Organization");

            childern.AddPermissin("添加", "Create");
        }
    }
}
