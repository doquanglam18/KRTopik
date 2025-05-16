using DATN.Application.Dtos.CommentDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.ReadingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.TestSetDtos
{
    public class DoTestSetDto
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public Guid? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public List<ListeningQuestionForTestDto>? listeningQuestions { get; set; }
        public List<ReadingQuestionForTestDto>? readingQuestions { get; set; }
        public string RankQuestionName { get; set; }

        public int AvgRating { get; set; }
    }
}
