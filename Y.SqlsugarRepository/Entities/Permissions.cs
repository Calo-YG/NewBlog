namespace Calo.Blog.EntityCore.Entities
{
    public class Permissions:PermissionsBase
    {
        public Permissions() { }
        public Permissions(string id,string name,string code,string? parentcode)
        {
            Id = id;
            Name = name;
            Code = code;
            ParentCode = parentcode;
        }
    }
}
