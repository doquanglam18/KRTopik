using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class RankQuestion : BaseEntity
    {
        public string RankQuestionName { get; set; }
        public string Note { get; set; }
        public List<ReadingQuestion>? ReadingQuestions { get; set; }
        public List<ListeningQuestion>? ListeningQuestions { get; set; }
        public List<TestSet>? TestSets { get; set; }
    }
}
