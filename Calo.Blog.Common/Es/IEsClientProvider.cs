using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.Common.ES
{
	public interface IEsClientProvider
	{
		ElasticClient Current { get; }
	}

	
}
