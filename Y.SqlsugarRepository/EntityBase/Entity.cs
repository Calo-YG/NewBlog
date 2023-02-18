using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Y.SqlsugarRepository.EntityAttribute;

namespace Y.SqlsugarRepository.EntityBase
{
    public class Entity<TPrimarykey> : IEntity<TPrimarykey> , IConcurrentToken
    {
        [PrimaryKey]
        public TPrimarykey Id { get; set; }
        [ConcurrentToken]
        public string? ConcurrentToken { get; set ; }
        public TPrimarykey? CreatorUserId { get; set; }
        public string? CreateorUserName { get; set; }
        public DateTime? CreattionTime { get; set; }
        public bool IsDeleted { get; set; }

        public TPrimarykey DeleteUserId { get; set; }
    }
}
