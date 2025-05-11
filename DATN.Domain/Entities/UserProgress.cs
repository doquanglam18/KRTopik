using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class UserProgress
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int TestSetId { get; set; }
        public int TotalQuestions { get; set; }
        public int CompletedQuestions { get; set; }
        public int BestResults { get; set; }
        public DateTime FirstAttemptAt { get; set; }
        public DateTime LastAttemptAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public bool IsDelete { get; set; }

        public User User { get; set; }

        public TestSet TestSet { get; set; }
    }

}
