namespace Calo.Blog.Common.Authorization.Authorize
{
    public class Permission
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string? ParentCode { get; set; }

        public bool IsGroup { get; set; }

        public List<Permission> Childrens { get; set; }
    }
}
