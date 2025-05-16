using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.ListeningDtos
{
    public class ListeningQuestionForTestDto
    {
        public string Question { get; set; }
        public string ListeningSoundURL { get; set; }
        public List<ListeningAnswerForTestDto> ListeningAnswers { get; set; }
    }
}
