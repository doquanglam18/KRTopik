using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Domain.Entities
{
    public class KoreaBlog : BaseEntity
    {
        public string Title { get; set; }

        public string TitleVietSub { get; set; }
        public string? BlogImageUrl { get; set; }

        public int View { get; set; }
        public string Content { get; set; }
        public string VietSubContent { get; set; }
        public Guid CreateadBy { get; set; }

        public bool IsActive { get; set; }
        public List<RatingBlog>? RatingBlogs { get; set;}
    }

}
