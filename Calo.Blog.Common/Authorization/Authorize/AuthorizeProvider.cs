namespace Calo.Blog.Common.Authorization.Authorize
{
    public  class AuthorizeProvider
    {
        public Permissions Permissions { get => _permissions; }

        private Permissions _permissions { get; set; }

        public AuthorizeProvider() 
        {
            _permissions = _permissions ?? new Permissions();
            InitPermissions();
        }

        public virtual void InitPermissions()
        {
            throw new NotImplementedException();
        }
    } 
}
