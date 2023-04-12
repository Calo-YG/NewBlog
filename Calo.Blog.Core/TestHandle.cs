using Calo.Blog.Common.Y.EventBus;
using Calo.Blog.EntityCore.DataBase.Entities;
using Microsoft.Extensions.Logging;
using Y.SqlsugarRepository.Repository;

namespace Calo.Blog.Domain
{
    public class TestHandle : EventHandle<User>, IEventHandle<User>
    {
        private readonly IBaseRepository<User, long> _repository;
        private readonly ILogger _logger;
        public TestHandle(ILoggerFactory loggerFactory,IBaseRepository<User,long> repository) : base(loggerFactory)
        {
            _repository = repository;
            _logger= loggerFactory.CreateLogger<TestHandle>();
        }

        public override async Task HandleAsync(User t)
        {
            var users = await _repository.GetListAsync();
            if (users.Any())
            {
                _logger.LogInformation("用户名" + users[0].UserName);
            }
        }
    }
}
