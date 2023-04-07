namespace Calo.Blog.Common.Authorization.Authorize
{
    public  class AuthorizeProvider
    {
        public Permission Permissions { get => _permissions; }

        private Permission _permissions { get; set; }

        public AuthorizeProvider() 
        {
            _permissions = _permissions ?? new Permission();
            InitPermissions();
        }

        public virtual void InitPermissions()
        {
            throw new NotImplementedException();
        }
    } 
}
