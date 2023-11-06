namespace Calo.Blog.Common.Authorization.Authorize
{
    public static class AuthorizePermissionExtensions
    {
        public static SystemPermission AddGroup(this SystemPermission permission,string code, string name)
        {
            var _permission = new SystemPermission { 
                Name = name, 
                Code = code, 
                ParentCode = permission
                .Code,IsGroup=true 
            };
            if(permission.Childrens == null)
            {
                permission.Childrens = new List<SystemPermission>();
            }
            permission.Childrens.Add(_permission);
            return _permission;
        }

        public static SystemPermission AddChild(this SystemPermission permission, string name,string code)
        {
            var child = new SystemPermission()
            {
                Name = name,
                Code=code,
                ParentCode = permission.Code,
                IsGroup=false
            };
            if (permission.Childrens == null)
            {
                permission.Childrens = new List<SystemPermission>();
            }
            permission.Childrens.Add(child);
            return child;
        }

        public static void AddPermissin(this SystemPermission permission,string code, string name)
        {
            var _permission = new SystemPermission()
            {
                Name = name,
                ParentCode = permission.Code,
                Code = code,
            };
            if (permission.Childrens == null)
            {
                permission.Childrens = new List<SystemPermission>();
            }
            permission.Childrens.Add(_permission);
        }
    }
}
