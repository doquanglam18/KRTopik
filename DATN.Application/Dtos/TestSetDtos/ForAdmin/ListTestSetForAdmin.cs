using DATN.Application.Dtos.CommentDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.TestSetDtos.ForAdmin
{
    public class ListTestSetForAdmin
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public Guid? CreatedBy { get; set; }
        public int CountQuestions { get; set; }
        public DateTime CreatedDate { get; set; }

        public int CountComment { get; set; }
        public int CountUserDoTest { get; set; }
        public string RankQuestionName { get; set; }
    }
}
