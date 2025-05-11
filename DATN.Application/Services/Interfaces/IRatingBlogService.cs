using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Interfaces
{
    public interface IRatingBlogService
    {
        Task<Result> AddRatingBlogAsync(RatingBlog ratingBlog);
        Task<Result> DeleteRatingBlogAsync(int id);
        Task<Result> UpdateRatingBlogAsync(RatingBlog ratingBlog);
        Task<RatingBlog> GetRatingBlogByIdAsync(int id);
    }
}
