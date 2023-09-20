using SqlSugar;
namespace Y.SqlsugarRepository.EntityBase
{
    public class AutiedEntity<TPrimarykey> : Entity<TPrimarykey>
        ,IMayHasCreatoer
        ,IMayHasCreatorUserName
        ,IMayHasDeleteTime
        ,ISoftDelete
        ,IMayHasDeleteUserId
    {

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

    public class AutiedEntity:Entity
        , IMayHasCreatoer
        , IMayHasCreatorUserName
        , IMayHasDeleteTime
        , ISoftDelete
        , IMayHasDeleteUserId
    {
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
