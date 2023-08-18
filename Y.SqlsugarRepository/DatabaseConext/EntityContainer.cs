namespace Y.SqlsugarRepository.DatabaseConext
{
    public class EntityContainer : IEntityContainer
    {
        public IReadOnlyList<Type> EntityTypes { get; set; }

        public EntityContainer(List<Type> entityTypes)
        {
            EntityTypes = entityTypes;
        }
    }
}
