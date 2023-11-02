namespace Calo.Blog.Common.Authorization.Authorize
{
    public static class AuthorizePermissionExtensions
    {
        public static Permission AddGroup(this Permission permission,string code, string name)
        {
            var _permission = new Permission { 
                Name = name, 
                Code = code, 
                ParentCode = permission
                .Code,IsGroup=true 
            };
            if(permission.Childrens == null)
            {
                permission.Childrens = new List<Permission>();
            }
            permission.Childrens.Add(_permission);
            return _permission;
        }

        public static Permission AddChild(this Permission permission, string name,string code)
        {
            var child = new Permission()
            {
                Name = name,
                Code=code,
                ParentCode = permission.Code,
                IsGroup=false
            };
            if (permission.Childrens == null)
            {
                permission.Childrens = new List<Permission>();
            }
            permission.Childrens.Add(child);
            return child;
        }

        public static void AddPermissin(this Permission permission,string code, string name)
        {
            var _permission = new Permission()
            {
                Name = name,
                ParentCode = permission.Code,
                Code = code,
            };
            if (permission.Childrens == null)
            {
                permission.Childrens = new List<Permission>();
            }
            permission.Childrens.Add(_permission);
        }
    }
}
