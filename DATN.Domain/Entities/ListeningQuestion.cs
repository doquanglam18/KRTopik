using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class ListeningQuestion : BaseEntity
    {
        public string Question { get; set; }
        public int? TestSetId { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public string ListeningSoundURL { get; set; }

        public string? ListeningScript { get; set; }
        public TestSet TestSet { get; set; }

        public bool IsPublic { get; set; }
        public List<ListeningAnswer> ListeningAnswers { get; set; }
        public int RankQuestionId { get; set; }
        public RankQuestion RankQuestion { get; set; }

    }

}
