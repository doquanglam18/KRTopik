using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.TestSetDtos
{
    public class ScoringResultDto
    {
        public int TotalQuestions { get; set; }
        public int CorrectCount { get; set; }
        public double ScorePercentage { get; set; }
        public List<int> CorrectAnswerIds { get; set; } = new();
    }

}
