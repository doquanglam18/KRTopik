using DATN.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.KoreaBlogDtos
{
    public class KoreaBlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleVietSub { get; set; }
        public string? BlogImageUrl { get; set; }
        public IFormFile BlogImage { get; set; }
        public int View { get; set; }
        public string Content { get; set; }
        public string VietSubContent { get; set; }
        public Guid CreateadBy { get; set; }
        public bool IsActive { get; set; }
        public List<RatingBlog>? RatingBlogs { get; set; }
    }
}
