using Microsoft.Extensions.DependencyInjection;
using Y.SqlsugarRepository.DatabaseConext;
using Y.SqlsugarRepository.Repository;

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
            services.AddSingleton<IPropriesSetValue, PropriesSetValue>();
            action.Invoke(entityProvider);
            new EntityRepositoryInjection(services, entityProvider).AddRepository();
        }

        public static void AddRepository(this IServiceCollection services, IEntityProvider entityProvider)
        {
            services.AddSingleton<IPropriesSetValue, PropriesSetValue>();
            new EntityRepositoryInjection(services, entityProvider).AddRepository();
        }

        public static IEntityRepositoryInjection RepositoryInjection(this IServiceCollection services, Action<IEntityProvider> action)
        {
            var entityProvider = new EntityProvider(services);
            services.AddSingleton<IPropriesSetValue, PropriesSetValue>();
            action.Invoke(entityProvider);
            return new EntityRepositoryInjection(services, entityProvider);
        }

        public static IEntityRepositoryInjection RepositoryInjection(this IServiceCollection services, IEntityProvider entityProvider)
        {
            services.AddSingleton<IPropriesSetValue, PropriesSetValue>();
            return new EntityRepositoryInjection(services, entityProvider);
        }

        public static IEntityRepositoryInjection RepositoryInjection(this IServiceCollection services)
        {
            services.AddSingleton<IPropriesSetValue, PropriesSetValue>();
            return new EntityRepositoryInjection(services);
        }

        public static void AddRepository(this IEntityRepositoryInjection entityRepository, Action<IEntityProvider> action)
        {
            IEntityProvider entityProvider = new EntityProvider(entityRepository.Services);
            entityRepository.Services.AddSingleton<IPropriesSetValue, PropriesSetValue>();
            action.Invoke(entityProvider);
            entityRepository.LoadEntity(entityProvider);
            entityRepository.AddRepository();
        }
    }
}
