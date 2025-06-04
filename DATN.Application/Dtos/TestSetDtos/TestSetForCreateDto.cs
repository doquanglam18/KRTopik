using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.TestSetDtos
{
    public class TestSetForCreateDto
    {
        public string TestName { get; set; }
        public Guid? CreatedBy { get; set; }
        public bool IsDelele { get; set; }
        public int RankQuestionId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

        public List<int> QuestionIds { get; set; }
    }
}
