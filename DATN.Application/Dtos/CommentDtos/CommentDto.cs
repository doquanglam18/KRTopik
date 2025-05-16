using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.CommentDtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserAvatar { get; set; }
    }
}
