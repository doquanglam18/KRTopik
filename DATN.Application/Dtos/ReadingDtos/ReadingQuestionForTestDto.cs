using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.ReadingDtos
{
    public class ReadingQuestionForTestDto
    {
        public string? Question { get; set; }
        public string? ReadingImageURL { get; set; }
        public List<ReadingAnswerForTestDto> ReadingAnswers { get; set; }
    }
}
