namespace Calo.Blog.Common.Authorization.Authorize
{
    public class AuthorizePermissionContext:IAuthorizePermissionContext
    {
        public static SystemPermission Permission { get;private set; }

        public SystemPermission DefinePermission { get => Permission; }
        public AuthorizePermissionContext()
        {
            if(Permission == null)
            {
                Permission = new SystemPermission();  
            }
        }

        public void AddGroup(string code,string name) => DefinePermission.AddGroup(code,name);
    }
}
