namespace FreeInterface.wyymusic
{
    public class WyyConsts
    {
        public const string WyyApiHost = "http://124.71.15.19:3000/";
        /// <summary>
        /// 手机号登录
        /// </summary>
        public const string PhoneLogin = "login/cellphone";
        /// <summary>
        /// 手机号登录验证码
        /// </summary>
        public const string PhoneCode = "captcha/sent";
        /// <summary>
        /// 验证验证码
        /// </summary>
        public const string CheckCode = "captcha/verify";
        /// <summary>
        /// 生成二维码Key
        /// </summary>
        public const string QrCodeKey = "login/qr/key";
        /// <summary>
        /// 生成二维码
        /// </summary>
        public const string QrCode = "login/qr/create";
        /// <summary>
        /// 检测二维码状态
        /// </summary>
        public const string ChcekQrCode = "login/qr/check";
        /// <summary>
        /// 游客登录
        /// </summary>
        public const string OrtherLoging = "register/anonimous";
        /// <summary>
        /// 刷新登录
        /// </summary>
        public const string RefreshLogin = "login/refresh";
        /// <summary>
        /// 登录状态
        /// </summary>
        public const string LoginStatu = "login/status";
        /// <summary>
        /// 退出登录
        /// </summary>
        public const string LogOut = "logout";
        /// <summary>
        /// 用户信息
        /// </summary>
        public const string UserInfo = "user/detail";
        /// <summary>
        /// 账号信息
        /// </summary>
        public const string AccountInfo = "user/account";
        /// <summary>
        /// 用户绑定信息
        /// </summary>
        public const string BindInfo = "user/binding";
        /// <summary>
        /// 私信和通知接口
        /// </summary>
        public const string Messages = "pl/count";
        /// <summary>
        /// 用户歌单列表
        /// </summary>
        public const string SongList = "user/playlist";
        /// <summary>
        /// 精品歌单
        /// </summary>
        public const string BoutiqueSongs = "top/playlist/highquality";
    }
}
