using Microsoft.Extensions.DependencyInjection;
using Y.SqlsugarRepository.DatabaseConext;

namespace Y.SqlsugarRepository
{
    /// <summary>
    /// 仓储注入扩展方法
    /// </summary>
    public static class DependdencyInjectionFactory
    {
        public static void AddRepository(this IServiceCollection services, Action<IEntityProvider> action)
        {
            var entityProvider = new EntityProvider(services);
            action.Invoke(entityProvider);
            new EntityRepositoryInjection(services, entityProvider).AddRepository();
        }

        public static void AddRepository(this IServiceCollection services, IEntityProvider entityProvider)
        {
            new EntityRepositoryInjection(services, entityProvider).AddRepository();
        }

        public static IEntityRepositoryInjection RepositoryInjection(this IServiceCollection services, Action<IEntityProvider> action)
        {
            var entityProvider = new EntityProvider(services);
            action.Invoke(entityProvider);
            return new EntityRepositoryInjection(services, entityProvider);
        }

        public static IEntityRepositoryInjection RepositoryInjection(this IServiceCollection services, IEntityProvider entityProvider)
        {
            return new EntityRepositoryInjection(services, entityProvider);
        }

        public static IEntityRepositoryInjection RepositoryInjection(this IServiceCollection services)
        {
            return new EntityRepositoryInjection(services);
        }

        public static void AddRepository(this IEntityRepositoryInjection entityRepository, Action<IEntityProvider> action)
        {
            IEntityProvider entityProvider = new EntityProvider(entityRepository.Services);
            action.Invoke(entityProvider);
            entityRepository.LoadEntity(entityProvider);
            entityRepository.AddRepository();
        }
    }
}
