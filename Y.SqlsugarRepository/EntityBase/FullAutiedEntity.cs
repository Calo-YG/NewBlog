using SqlSugar;

namespace Y.SqlsugarRepository.EntityBase
{
    public class FullAutiedEntity<TPrimarykey> : AutiedEntity<TPrimarykey>, IMayHasUpateUserId,IMayHasUpdateTime
    {
        [SugarColumn(IsNullable = true)]
        public DateTime? UpdateTime { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? UpdateUserId { get;set; }
    }
}
