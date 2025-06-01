using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.TestSetDtos
{
    public class SubmitTestDto
    {
        public int TestSetId { get; set; }
        public List<AnswerDto> Answers { get; set; }

        public DateTime FirstAttemptAt { get; set; }
        public DateTime LastAttemptAt { get; set; }
    }

    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public int SelectedAnswerId { get; set; }
    }
}
