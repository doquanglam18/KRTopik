using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.UserProgressDtos
{
    public class CreateUserProgressDto
    {
        public Guid UserId { get; set; }
        public int TestSetId { get; set; }
        public int TotalQuestions { get; set; }
        public int CompletedQuestions { get; set; }
        public int BestResults { get; set; }
        public DateTime FirstAttemptAt { get; set; }
        public DateTime LastAttemptAt { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
