using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class TestSet : BaseEntity
    {
        public string TestName { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDelele { get; set; }
        public List<ListeningQuestion>? ListeningQuestions { get; set; }

        public List<ReadingQuestion>? ReadingQuestions { get; set; }
        public List<Comment> Comments { get; set; }
        public List<UserProgress> UserProgress { get; set; }
        public int RankQuestionId { get; set; }
        public RankQuestion RankQuestion { get; set; }
    }

}
