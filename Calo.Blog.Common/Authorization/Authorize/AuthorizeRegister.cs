using System.Linq.Expressions;

namespace Calo.Blog.Common.Authorization.Authorize
{
    public class AuthorizeRegister
    {
        public static List<AuthorizeProvider> AuthorizeProviders { get; private set; }

        public AuthorizeRegister() 
        {        
            if(AuthorizeProviders is null) throw new ArgumentNullException(nameof(AuthorizeProviders));
        }

        static AuthorizeRegister()
        {
            AuthorizeProviders = AuthorizeProviders ?? new List<AuthorizeProvider>();
        }

        
        public static void RegisterAuthorizeProvider<T>() where T : AuthorizeProvider
        {
            var instance = CreateInstance<T>();
            AuthorizeProviders.Add(instance);
        }

        public virtual void RegisterAuthorize<T>() where T : AuthorizeProvider
        {
            var instance =CreateInstance<T>();
            AuthorizeProviders.Add(instance);
        }

        public static T CreateInstance<T>()
        {
            var tye = typeof(T);
            var newExpre = Expression.New(tye);
            var instance = Expression.Lambda<Func<T>>(newExpre).Compile();
            return instance.Invoke();
        }
    }
}