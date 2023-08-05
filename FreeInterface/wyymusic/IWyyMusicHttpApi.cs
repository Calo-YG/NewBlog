using FreeInterface.wyymusic.Models;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace FreeInterface.wyymusic
{
	[HttpHost(WyyConsts.WyyApiHost)]
	public interface IWyyMusicHttpApi : IHttpApi
	{
		[HttpGet(WyyConsts.QrCodeKey)]
		public Task<WyyResponse<QrCodeKey>> GetWyyKey(long timestamp);

		[HttpGet(WyyConsts.QrCode)]
		public Task<WyyResponse<QrCode>> GetQrCode (string key,long timestamp,bool qrimg=true);
		/// <summary>
		/// 发送手机验证码
		/// </summary>
		/// <param name="phone"></param>
		/// <returns></returns>
		[HttpGet(WyyConsts.PhoneCode)]
		public Task<WyyResponse<bool>> SentCode(string phone);
		[HttpGet(WyyConsts.CheckCode)]
		public Task<PhoneLoginCode> CheckPhoneLogin(string phone, string code);
		/// <summary>
		/// 检测二维码扫描状态
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		[HttpGet(WyyConsts.ChcekQrCode)]
		public Task<CheckCode> CheckQrCode(string key,long timestamp,bool noCookie = true);
		/// <summary>
		/// 检测状态
		/// </summary>
		/// <param name="timestamp"></param>
		/// <returns></returns>
		[HttpGet(WyyConsts.LoginStatus)]
		public Task<WyyResponse<StatusResult>> CheckStatus(long timestamp);
	}
}
