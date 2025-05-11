using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
    public class UserService : IUserService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> AddUserAsync(User user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.FullName))
                    return Result.Failure("Họ và tên không được để trống.");

                if (string.IsNullOrWhiteSpace(user.Email))
                    return Result.Failure("Email không được để trống.");

                if (string.IsNullOrWhiteSpace(user.PasswordHash))
                    return Result.Failure("Mật khẩu không được để trống.");

                if (user.DateOfBirth == default)
                    return Result.Failure("Ngày sinh không hợp lệ.");

                var isEmailTaken = await _unitOfWork.UserRepository
                                       .GetAll()
                                       .AnyAsync(u => u.Email == user.Email);

                if (isEmailTaken)
                {
                    return Result.Failure("Email đã được đăng ký.");
                }
                   

                user.Id = Guid.NewGuid();
                user.CreatedDate = DateTime.UtcNow;
                user.UpdatedDate = DateTime.UtcNow;
                user.IsActive = false;
                user.NumberOfContributions = 0;


                await _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Đăng ký người dùng thành công!");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Có lỗi khi đăng ký: {ex.Message}");
            }
        }


        public async Task<Result> DeleteUserAsync(Guid id)
        {
            User user = await GetUserByIdAsync(id);
            try
            {
                if (user == null)
                {
                    return Result.Failure("Không tìm thấy người dùng.");
                }
   
                user.IsActive = !user.IsActive;
                await _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Thay đổi trạng thái người dùng thành công !.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Đã xảy ra lỗi khi thay đổi trạng thái người dùng: {ex.Message}");
            }

        }

        public async Task<ResultV<User>> CheckLogin(string email, string passWord)
        {

            var user = await _unitOfWork.UserRepository.GetUserForLogin(email, passWord);
            if (user == null)
            {
                return ResultV<User>.Failure("Tài khoản hoặc mật khẩu không chính xác !");

            }
            else
            {
                return ResultV<User>.Success(user);
            }
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAll().ToListAsync();
            return users;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
            return user;
        }




        public async Task<Result> UpdateUserAsync(User user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.FullName))
                    return Result.Failure("Họ và tên không được để trống.");

                if (string.IsNullOrWhiteSpace(user.PasswordHash))
                    return Result.Failure("Mật khẩu không được để trống.");

                if (user.DateOfBirth == default)
                    return Result.Failure("Ngày sinh không hợp lệ.");


                await _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Thay đổi thông tin người dùng thành công!");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Có lỗi khi thay đổi thông tin: {ex.Message}");
            }
        }
    }
}
