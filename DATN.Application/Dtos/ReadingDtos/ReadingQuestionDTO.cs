using DATN.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.ReadingDtos
{
    public class ReadingQuestionDto
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? ReadingImageURL { get; set; }

        public IFormFile? Image { get; set; } 
        public int? TestSetId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public bool IsPublic { get; set; }

        public string? TestSetName { get; set; }
        public List<ReadingAnswerDTO> ReadingAnswers { get; set; }

        public int RankQuestionId { get; set; }
        public string? RankQuestionName { get; set; }
    }
}
