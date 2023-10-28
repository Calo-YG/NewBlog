namespace Y.SqlsugarRepository.EntityAttribute
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =false)]
    public class StringAtrribute:Attribute
    {
        public int _length { get; private set; }

        public string _Type { get; private set; }
      
        public  StringAtrribute(int length = 200 , string type = "nvarchar")
        {
            _length= length;
            _Type = type;
        }
    }
}
