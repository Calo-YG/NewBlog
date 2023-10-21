namespace Calo.Blog.Common.Authorization
{
    public class PermissionCheck : IPermissionCheck
    {
        public virtual bool IsGranted(UserTokenModel userTokenModel, string[] authorizationNames)
        {
            var array = new string[] { "tttt" };
            return array.Contains(authorizationNames[0]);
        }
    }
}
