using Masuit.Tools.Systems;

namespace Y.Blog.Host.Data.DataShared
{
    /// <summary>
    /// 当前用户的
    /// </summary>
    public class CurrentUserHubInfo
    {
        public string HubClentId { get {
                var sf = SnowFlake.GetInstance();
                string token = sf.GetUniqueId();// rcofqodori0w
                return sf.GetUniqueShortId(8);// qodw9728
            } 
        }
    }
}
