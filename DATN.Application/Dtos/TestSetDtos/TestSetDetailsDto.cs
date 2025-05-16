using DATN.Application.Dtos.CommentDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.TestSetDtos
{
    public class TestSetDetailsDto
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public Guid? CreatedBy { get; set; }
        public int CountListeningQuestion { get; set; }
        public int CountReadingQuestion { get; set; }
        public List<CommentDto> Comments { get; set; }
        public int CountUserDo { get; set; }
        public string RankQuestionName { get; set; }

        public int AvgRating { get; set; }
    }
}
