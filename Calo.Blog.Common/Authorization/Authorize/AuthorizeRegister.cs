using System.Linq.Expressions;

namespace Calo.Blog.Common.Authorization.Authorize
{
    public class AuthorizeRegister
    {
        public static List<IAuthorizePermissionProvider> AuthorizeProviders { get; private set; }

        public static List<Permission> Permissions { get; private set; }

        private readonly IAuthorizePermissionContext Context;
 
        public AuthorizeRegister() 
        {        
            if(AuthorizeProviders is null) throw new ArgumentNullException(nameof(AuthorizeProviders));
            Context = new AuthorizePermissionContext();
        }

        static AuthorizeRegister()
        {
            AuthorizeProviders = AuthorizeProviders ?? new List<IAuthorizePermissionProvider>();
            Permissions = Permissions?? new List<Permission>(); 
        }

        
        public static void RegisterAuthorizeProvider<T>() where T : IAuthorizePermissionProvider
        {
            var instance = CreateInstance<T>();
            AuthorizeProviders.Add(instance);
        }

        public virtual void RegisterAuthorize<T>() where T : IAuthorizePermissionProvider
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

        public virtual List<Permission> InitAuthorizePermission()
        {
            if (AuthorizeProviders is null) throw new ArgumentNullException(nameof(AuthorizeProviders));
            List<Permission> permissions = new List<Permission>();  
            foreach(var provider in AuthorizeProviders)
            {
                if(provider is IAuthorizePermissionProvider permissionProvider)
                {
                    provider.PermissionDefined(Context);
                }
            }
            return permissions;
        }
    }
}