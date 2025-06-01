using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.ListeningDtos.ForAddTestSet
{
    public class ListeningAwDto
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public bool IsCorrect { get; set; } 
    }
}
