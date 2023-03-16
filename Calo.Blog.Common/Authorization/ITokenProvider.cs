
namespace Calo.Blog.Common.Authorization
{
    public interface ITokenProvider
    {
        string GenerateToken(UserTokenModel user);
    }
}
