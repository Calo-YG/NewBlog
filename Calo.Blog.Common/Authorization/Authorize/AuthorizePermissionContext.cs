namespace Calo.Blog.Common.Authorization.Authorize
{
    public class AuthorizePermissionContext:IAuthorizePermissionContext
    {
        public static Permission Permission { get;private set; }

        public Permission DefinePermission { get => Permission; }
        public AuthorizePermissionContext()
        {
            if(Permission == null)
            {
                Permission = new Permission();  
            }
        }

        static AuthorizePermissionContext()
        {
            Permission = new Permission();
        }

        public void AddGroup(string code,string name) => DefinePermission.AddGroup(code,name);
    }
}
