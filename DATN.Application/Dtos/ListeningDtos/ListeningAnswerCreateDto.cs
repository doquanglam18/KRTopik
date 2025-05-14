using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.ListeningDtos
{
    public class ListeningAnswerCreateDto
    {
        public IFormFile? Image { get; set; }
        public string? Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}
