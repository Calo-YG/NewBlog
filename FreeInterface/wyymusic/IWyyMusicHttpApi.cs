using WebApiClientCore;
using WebApiClientCore.Attributes;

namespace FreeInterface.wyymusic
{
	[HttpHost("https://music.163.com")]
	public interface IWyyMusicHttpApi : IHttpApi
	{
	}
}
