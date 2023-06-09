﻿using SqlSugar;
using Y.SqlsugarRepository.EntityAttribute;

namespace Y.SqlsugarRepository.EntityBase
{
    public class Entity<TPrimarykey> : IEntity<TPrimarykey>, IConcurrentToken
    {
        public TPrimarykey Id { get; set; }

        [ConcurrentToken]
        public string? ConcurrentToken { get; set; }

        [SugarColumn(IsNullable = true)]
        public TPrimarykey? CreatorUserId { get; set; }

        [SugarColumn(IsNullable = true)]
        public string? CreateorUserName { get; set; }

        [SugarColumn(IsNullable = true)]
        public DateTime? CreationTime { get; set; }
        public bool IsDeleted { get; set; }

        [SugarColumn(IsNullable = true)]
        public TPrimarykey? DeleteUserId { get; set; }
    }
}
