namespace Y.SqlsugarRepository.EntityAttribute
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    public class PrimaryKeyAttribute:Attribute
    {
        public bool Enabled { get; }

        private bool _enabled = false;

        public PrimaryKeyAttribute() { }    

        public PrimaryKeyAttribute(bool enabled)
        { 
            _enabled= enabled;
        }
    }
}
