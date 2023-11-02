namespace Calo.Blog.Common.Authorization.Authorize
{
    public class Permission
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string? ParentCode { get; set; }
         List<Permission>  Groups { get; set; }
         List<Permission> Childrens { get; set; }



        public Permission AddGroup(string code,string name)
        {
            var permission= new Permission { Name = name, Code = code,ParentCode = this.ParentCode};
            Groups.Add(permission);
            return permission;
        }

        public virtual Permission AddChild(string name)
        {
            var childe = new Permission()
            {
                Name = name,
                ParentCode = this.ParentCode
            };
            Childrens.Add(childe);
            return childe;
        }

        public virtual void AddPermissin(string code,string name)
        {
            var permission = new Permission()
            {
                Name = name,
                ParentCode = this.ParentCode
            };
        }
    }
}
