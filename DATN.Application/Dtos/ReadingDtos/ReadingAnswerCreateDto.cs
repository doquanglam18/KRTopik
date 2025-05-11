using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.ReadingDtos
{
    public class ReadingAnswerCreateDto
    {
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}

