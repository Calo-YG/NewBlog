using Y.SqlsugarRepository.Repository;

namespace Y.SqlsugarRepository
{
    public static class AutoRegisterRepository
    {
        public static RepositoryDependencyInjection Default { get; }
        static AutoRegisterRepository()
        {
            Default = new RepositoryDependencyInjection(typeof(IBaseRepository<>), typeof(IBaseRepository<,>), typeof(BaseRepository<>), typeof(BaseRepository<,>));
        }
    }
}
