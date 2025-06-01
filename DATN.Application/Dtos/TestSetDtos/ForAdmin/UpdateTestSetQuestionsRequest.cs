using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.TestSetDtos.ForAdmin
{
    public class UpdateTestSetQuestionsRequest
    {
        public int TestSetId { get; set; }
        public List<int> QuestionIds { get; set; }
        public int RankQuestionId { get; set; }
    }
}
