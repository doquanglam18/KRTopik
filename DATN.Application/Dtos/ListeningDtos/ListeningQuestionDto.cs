using DATN.Application.Dtos.ReadingDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.ListeningDtos
{
    public class ListeningQuestionDto
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? ListeningSoundURL { get; set; }

        public IFormFile? Sound { get; set; }
        public int? TestSetId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string? ListeningScript { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public bool IsPublic { get; set; }

        public string? TestSetName { get; set; }
        public List<ListeningAnswerDto> ListeningAnswers { get; set; }

        public int RankQuestionId { get; set; }
        public string? RankQuestionName { get; set; }
    }
}
