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

		[HttpGet(WyyConsts.CheckCode)]
		public Task<CheckCode> ChckeCode(string key);

	}
}
