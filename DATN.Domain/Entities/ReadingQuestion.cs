using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class ReadingQuestion : BaseEntity
    {
        public string? Question { get; set; }
        public string? ReadingImageURL { get; set; }
        public int? TestSetId { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public bool IsPublic { get; set; } 
        public TestSet TestSet { get; set; }
        public List<ReadingAnswer> ReadingAnswers { get; set; }
        public int RankQuestionId { get; set; }
        public RankQuestion RankQuestion { get; set; }

    }

}
