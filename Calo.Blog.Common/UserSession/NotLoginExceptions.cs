
namespace Calo.Blog.Common.UserSession
{
    public class NotLoginExceptions : Exception
    {
        public NotLoginExceptions(string message) : base(message)
        {
        }

        public NotLoginExceptions(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public static class NotLoginExceptionsExtensions
    {
        public static void ThrowNotloginExceptions()
        {
            throw new NotLoginExceptions("请先登录系统");
        }
    }
}
