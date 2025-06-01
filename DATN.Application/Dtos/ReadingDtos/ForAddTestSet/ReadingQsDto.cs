using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.ReadingDtos.ForAddTestSet
{
    public class ReadingQsDto
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public string? ReadingImageURL { get; set; }
        public List<ReadingAwDto> ReadingAnswers { get; set; }
    }
}
