using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
    public class RatingBlogService : IRatingBlogService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public RatingBlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> AddRatingBlogAsync(RatingBlog ratingBlog)
        {
            try
            {
                var errors = new List<string>();

                if (ratingBlog.UserId == Guid.Empty)
                    errors.Add("Người dùng không hợp lệ.");

                if (ratingBlog.BlogId <= 0)
                    errors.Add("Blog không hợp lệ.");

                if (string.IsNullOrWhiteSpace(ratingBlog.Content))
                    errors.Add("Nội dung đánh giá không được để trống.");

                if (ratingBlog.Rating < 1 || ratingBlog.Rating > 5)
                    errors.Add("Số sao đánh giá phải nằm trong khoảng từ 1 đến 5.");

                if (errors.Any())
                {
                    string errorMessage = string.Join(" ", errors);
                    return Result.Failure(errorMessage);
                }

                await _unitOfWork.RatingBlogRepository.Add(ratingBlog);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Thêm đánh giá thành công.");
            }
            catch (Exception ex)
            {
                return Result.Failure("Đã xảy ra lỗi khi thêm đánh giá: " + ex.Message);
            }
        }


        public async Task<Result> DeleteRatingBlogAsync(int id)
        {
            try
            {
                var rating = await _unitOfWork.RatingBlogRepository.GetByIdAsync(id);
                if (rating == null)
                {
                    return Result.Failure("Không tìm thấy đánh giá cần xóa.");
                }

                await _unitOfWork.RatingBlogRepository.Delete(rating);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Xóa đánh giá thành công.");
            }
            catch (Exception ex)
            {
                return Result.Failure("Đã xảy ra lỗi khi xóa đánh giá: " + ex.Message);
            }
        }


        public async Task<RatingBlog> GetRatingBlogByIdAsync(int id)
        {
            var rating = await _unitOfWork.RatingBlogRepository.GetByIdAsync(id);
            return rating;
        }

        public async Task<Result> UpdateRatingBlogAsync(RatingBlog ratingBlog)
        {
            try
            {
                var errors = new List<string>();

                if (ratingBlog.UserId == Guid.Empty)
                    errors.Add("Người dùng không hợp lệ.");

                if (ratingBlog.BlogId <= 0)
                    errors.Add("Blog không hợp lệ.");

                if (string.IsNullOrWhiteSpace(ratingBlog.Content))
                    errors.Add("Nội dung đánh giá không được để trống.");

                if (ratingBlog.Rating < 1 || ratingBlog.Rating > 5)
                    errors.Add("Số sao đánh giá phải nằm trong khoảng từ 1 đến 5.");

                if (errors.Any())
                {
                    string errorMessage = string.Join(" ", errors);
                    return Result.Failure(errorMessage);
                }

                await _unitOfWork.RatingBlogRepository.Update(ratingBlog);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Cập nhật đánh giá thành công.");
            }
            catch (Exception ex)
            {
                return Result.Failure("Đã xảy ra lỗi khi cập nhật đánh giá: " + ex.Message);
            }
        }

    }
}
