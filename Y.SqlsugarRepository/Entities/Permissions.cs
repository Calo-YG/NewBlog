namespace Calo.Blog.EntityCore.Entities
{
    public class Permissions:PermissionsBase
    {
        public Permissions() { }
        public Permissions(string id,string name,string code,string? parentcode,bool group,bool page,bool button)
        {
            Id = id;
            Name = name;
            Code = code;
            ParentCode = parentcode;
            Group = group;
            Page = page;
            Button = button;
        }
    }
}
