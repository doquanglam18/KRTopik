using DATN.Application.Dtos.CommentDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.ReadingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.TestSetDtos.ForAdmin
{
    public class TestSetDetailsForAdmin
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool IsDelele { get; set; }
        public List<ListeningQuestionForTestDto>? ListeningQuestions { get; set; }

        public List<ReadingQuestionForTestDto>? ReadingQuestions { get; set; }
        public List<CommentDto> Comments { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CountUserDoTest { get; set; }
        public string RankQuestionName { get; set; }

        public int RankQuestionId { get; set; }
    }
}
