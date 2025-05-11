using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class ReadingAnswer : BaseEntity
    {
        public int ReadingQuestionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        public ReadingQuestion ReadingQuestion { get; set; }
    }

}
