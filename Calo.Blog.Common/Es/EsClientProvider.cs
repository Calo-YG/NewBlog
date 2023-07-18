using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.ES
{
	public class EsClientProvider:IEsClientProvider
	{
		private readonly IOptions<ElstaicSearchConfig> _options;

		public ElasticClient Current => Init() ;
		public EsClientProvider(IOptions<ElstaicSearchConfig> options)
		{
			_options = options;
		}

		public ElasticClient Init()
		{
			var value = _options.Value;

			if (value is null)
			{
				throw new ArgumentNullException(nameof(value));	
			}

			CheckUrls(value.Urls);

			var uris = value.Urls.Select(p => new Uri(p));

			var connectionPool = new SniffingConnectionPool(uris);

			var connectionSetting = new ConnectionSettings(connectionPool);

			connectionSetting.BasicAuthentication(value.UserName, value.Password);

			return new ElasticClient(connectionSetting);
		}

		private void CheckUrls(List<string> urls)
		{
			if (urls?.Any() ?? false)
			{
				throw new ArgumentNullException(nameof(_options));
			}
		}
	}
}
