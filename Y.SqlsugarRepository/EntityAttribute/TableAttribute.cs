namespace Y.SqlsugarRepository.EntityAttribute
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false)]
    public class TableAttribute : Attribute
    {
        public string Name { get; private set; }

        public TableAttribute(string name)
        {
            Name = name;
        }
    }
}
