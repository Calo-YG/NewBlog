using FreeInterface.wyymusic.Models;
using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace FreeInterface.wyymusic
{
	[HttpHost(WyyConsts.WyyApiHost)]
	public interface IWyyMusicHttpApi : IHttpApi
	{
		[HttpGet(WyyConsts.QrCodeKey)]
		public Task<WyyResponse<QrCodeKey>> GetWyyKey();

		[HttpGet(WyyConsts.QrCode)]
		public Task<WyyResponse<QrCode>> GetQrCode (string key,bool qrimg=true);
		/// <summary>
		/// 发送手机验证码
		/// </summary>
		/// <param name="phone"></param>
		/// <returns></returns>
		[HttpGet(WyyConsts.PhoneCode)]
		public Task<WyyResponse<bool>> SentCode(string phone);
		[HttpGet(WyyConsts.CheckCode)]
		public Task<PhoneLoginCode> CheckPhoneLogin(string phone, string code);

	}
}
