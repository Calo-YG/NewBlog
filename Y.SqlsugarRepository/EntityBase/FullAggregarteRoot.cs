using SqlSugar;
using Y.SqlsugarRepository.EntityAttribute;

namespace Y.SqlsugarRepository.EntityBase
{
    public class FullAggregarteRoot : IFullAggregateRoot
        , IMayHasCreatoer
        , IMayHasCreatorUserName
        , IMayHasDeleteTime
        , ISoftDelete
        , IMayHasDeleteUserId
        , IConcurrentToken
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        [ConcurrentToken]
        public string? ConcurrentToken { get; set; }

        [SugarColumn(IsNullable = true)]
        public string? CreatorUserId { get; set; }

        [SugarColumn(IsNullable = true)]
        public string? CreatorUserName { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? CreationTime { get; set; }
        public bool IsDeleted { get; set; }

        [SugarColumn(IsNullable = true)]
        public string? DeleteUserId { get; set; }
        [SugarColumn(IsNullable = true)]
        public DateTime? DeleteTime { get; set; }
    }


    public class FullAggregarteRoot<TPrimaryKey> : IFullAggregateRoot<TPrimaryKey>
    , IMayHasCreatoer
    , IMayHasCreatorUserName
    , IMayHasDeleteTime
    , ISoftDelete
    , IMayHasDeleteUserId
    , IConcurrentToken
    {
        [PrimaryKey]
        public TPrimaryKey Id { get; set; }
        [ConcurrentToken]
        public string? ConcurrentToken { get; set; }

        [SugarColumn(IsNullable = true)]
        public string? CreatorUserId { get; set; }

        [SugarColumn(IsNullable = true)]
        public string? CreatorUserName { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? CreationTime { get; set; }
        public bool IsDeleted { get; set; }

        [SugarColumn(IsNullable = true)]
        public string? DeleteUserId { get; set; }
        [SugarColumn(IsNullable = true)]
        public DateTime? DeleteTime { get; set; }
    }
}
