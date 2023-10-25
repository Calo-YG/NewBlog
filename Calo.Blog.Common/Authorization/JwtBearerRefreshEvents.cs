namespace Calo.Blog.Common.Authorization
{
    public class JwtBearerRefreshEvents
    {
        public Func<RefreshTokenValidateFaileContext, Task> RefreshMessageRecived { get; set; }= (context)=> Task.CompletedTask;
    }
}
