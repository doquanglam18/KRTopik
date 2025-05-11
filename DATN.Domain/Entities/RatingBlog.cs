using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class RatingBlog : BaseEntity
    {
        public Guid UserId { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }

        [Range(1, 5, ErrorMessage = "Rating phải nằm từ 1 đến 5.")]
        public int Rating { get; set; }

        public User User { get; set; }
        public KoreaBlog KoreaBlog { get; set; }
    }

}
