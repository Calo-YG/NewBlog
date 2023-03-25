using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo.Blog.EntityCore.DadaSeed
{
    public class DataBaseSeed
    {
        private readonly ISqlSugarClient _client;
        private readonly IServiceProvider _serviceProvider;
        public DataBaseSeed(ISqlSugarClient client,IServiceProvider serviceProvider) 
        {
            _client = client;
            _serviceProvider = serviceProvider;
        }

        public void Create()
        {
            _client.Ado.BeginTran();
            try
            {
                new UserSeed(_client).Create();
            }
            catch (Exception)
            {
                _client.Ado.RollbackTran();
                throw;
            }
            _client.Ado.CommitTran();
        }
    }
}
