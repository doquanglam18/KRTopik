using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public Guid UserId { get; set; }
        public int TestSetId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public User User { get; set; }
        public TestSet TestSet { get; set; }
    }

}
