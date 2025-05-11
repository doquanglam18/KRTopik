using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.Repository.Interfaces;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
    public class KoreaBlogService : IKoreaBlogService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public KoreaBlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> AddKoreaBlogAsync(KoreaBlog koreaBlog)
        {
            try
            {
                var errors = new List<string>();

                if (string.IsNullOrWhiteSpace(koreaBlog.Title))
                    errors.Add("Tiêu đề không được để trống.");

                if (koreaBlog.Title?.Length > 200)
                    errors.Add("Tiêu đề không được vượt quá 200 ký tự.");

                if (string.IsNullOrWhiteSpace(koreaBlog.Content))
                    errors.Add("Nội dung tiếng Hàn không được để trống.");

                if (string.IsNullOrWhiteSpace(koreaBlog.VietSubContent))
                    errors.Add("Nội dung tiếng Việt không được để trống.");

                if (koreaBlog.CreateadBy == Guid.Empty)
                    errors.Add("Người tạo không hợp lệ.");

                if (errors.Any())
                {
                    string errorMessage = string.Join(" ", errors);
                    return Result.Failure(errorMessage);
                }

                await _unitOfWork.KoreaBlogRepository.Add(koreaBlog);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Thêm blog thành công.");
            }
            catch (Exception ex)
            {
                return Result.Failure("Đã xảy ra lỗi khi thêm blog: " + ex.Message);
            }
        }




        public async Task<Result> DeleteKoreaBlogAsync(int id)
        {
            try
            {
                var blog = await _unitOfWork.KoreaBlogRepository.GetByIdAsync(id);
                if (blog == null)
                {
                    return Result.Failure("Không tìm thấy blog cần xóa.");
                }

                await _unitOfWork.KoreaBlogRepository.Delete(blog);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Xóa blog thành công.");
            }
            catch (Exception ex)
            {
                return Result.Failure("Đã xảy ra lỗi khi xóa blog: " + ex.Message);
            }
        }


        public async Task<IEnumerable<KoreaBlog>> GetAllKoreaBlogAsync()
        {
            var blogs = await _unitOfWork.KoreaBlogRepository.GetAll().ToListAsync();
            return blogs;
        }

        public async Task<KoreaBlog> GetKoreaBlogByIdAsync(int id)
        {
            var blog = await _unitOfWork.KoreaBlogRepository.GetByIdAsync(id);
            return blog;
        }

        public async Task<Result> UpdateKoreaBlogAsync(KoreaBlog koreaBlog)
        {
            try
            {
                var errors = new List<string>();

                if (koreaBlog.Id <= 0)
                    errors.Add("Id blog không hợp lệ.");

                if (string.IsNullOrWhiteSpace(koreaBlog.Title))
                    errors.Add("Tiêu đề không được để trống.");

                if (koreaBlog.Title?.Length > 200)
                    errors.Add("Tiêu đề không được vượt quá 200 ký tự.");

                if (string.IsNullOrWhiteSpace(koreaBlog.Content))
                    errors.Add("Nội dung tiếng Hàn không được để trống.");

                if (string.IsNullOrWhiteSpace(koreaBlog.VietSubContent))
                    errors.Add("Nội dung tiếng Việt không được để trống.");


                if (errors.Any())
                {
                    return Result.Failure(string.Join(" ", errors));
                }

                await _unitOfWork.KoreaBlogRepository.Update(koreaBlog);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Cập nhật blog thành công.");
            }
            catch (Exception ex)
            {
                return Result.Failure("Đã xảy ra lỗi trong quá trình cập nhật blog: " + ex.Message);
            }
        }


    }
}
