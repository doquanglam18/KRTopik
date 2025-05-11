using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class ListeningAnswer : BaseEntity
    {
        public int ListeningQuestionId { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public ListeningQuestion ListeningQuestion { get; set; }

    }

}
