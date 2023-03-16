namespace Calo.Blog.Common.Authorization
{
    public class JwtSetting
    {
        public virtual string SecretKey { get; set; }

        public virtual string Issuer { get; set; }

        public virtual string Audience { get; set; }
    }
}
