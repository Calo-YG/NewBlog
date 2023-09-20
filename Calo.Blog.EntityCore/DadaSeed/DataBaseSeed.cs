using Calo.Blog.EntityCore.DataBase.Entities;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.Repository;

namespace Calo.Blog.EntityCore.DadaSeed
{
    public class DataBaseSeed
    {
        private readonly ISqlSugarClient _client;
        private readonly IServiceProvider _serviceProvider;
        private readonly IBaseRepository<User, Guid> baseRepository;
        public DataBaseSeed(ISqlSugarClient client,IServiceProvider serviceProvider) 
        {
            _client = client;
            _serviceProvider = serviceProvider;
            using var scope = _serviceProvider.CreateAsyncScope();

            baseRepository = scope.ServiceProvider.GetRequiredService<IBaseRepository<User, Guid>>();
        }

        public async Task Create()
        {
            _client.Ado.BeginTran();
            try
            {
              await   new UserSeed(baseRepository).Create();
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
