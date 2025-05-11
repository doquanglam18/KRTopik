using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.ReadingDtos
{
    public class ReadingQuestionCreateDto
    {
        public string Question { get; set; }
        public int RankQuestionId { get; set; }
        public IFormFile? Image { get; set; } // Dành cho WebApp gửi lên file ảnh

        public string? ReadingImageURL { get; set; } // Dành cho API để lưu URL ảnh từ Cloudinary

        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


        public List<ReadingAnswerCreateDto> ReadingAnswers { get; set; }
    }

}
