using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.GeminiDtos.ResponseDtos
{
    public class Content
    {
        public string role { get; set; }
        public List<Part> parts { get; set; }
    }
}
