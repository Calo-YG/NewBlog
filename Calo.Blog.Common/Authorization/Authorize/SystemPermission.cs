namespace Calo.Blog.Common.Authorization.Authorize
{
    public class SystemPermission
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string? ParentCode { get; set; }

        public bool IsGroup { get; set; }

        public List<SystemPermission>? Childrens { get; set; }
    }
}
