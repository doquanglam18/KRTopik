using DATN.Application.Dtos.ReadingDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.ListeningDtos
{
    public class ListeningQuestionCreateDto
    {
        public string Question { get; set; }
        public int RankQuestionId { get; set; }
        public IFormFile Sound { get; set; }

        public string? ListeningScript { get; set; }
        public string? ListeningSoundURL { get; set; } 

        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


        public List<ListeningAnswerCreateDto> ListeningAnswers { get; set; }
    }
}
