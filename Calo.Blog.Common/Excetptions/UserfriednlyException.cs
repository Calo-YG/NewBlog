namespace Calo.Blog.Common.Excetptions
{
    public class UserfriednlyException:Exception
    {
        public UserfriednlyException() { }

        public UserfriednlyException(string message) : base(message) { }
    }

    public static class ThrowUserFriendlyException
    {
        public static void ThrowException(string message)
        {
            throw new UserfriednlyException(message);
        }
    }

}
