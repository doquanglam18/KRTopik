using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.KoreaBlogDtos
{
    public class RatingBlogDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public DateTime  CreatedDate { get; set; }

        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
    }
}
