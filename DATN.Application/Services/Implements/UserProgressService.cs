using AutoMapper;
using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Dtos.TestSetDtos;
using DATN.Application.Dtos.TestSetDtos.ForAdmin;
using DATN.Application.Dtos.UserProgressDtos;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace DATN.Application.Services.Implements
{

    public class UserProgressService : IUserProgressService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserProgressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> CreateUserProgressAsync(UserProgress userProgress)
        {
            // Validate DTO
            if (userProgress.UserId == Guid.Empty)
                return Result.Failure("Người dùng không hợp lệ.");

            if (userProgress.TestSetId <= 0)
                return Result.Failure("TestSet không hợp lệ.");

            if (userProgress.TotalQuestions <= 0)
                return Result.Failure("Tổng số câu hỏi phải lớn hơn 0.");

            if (userProgress.CompletedQuestions < 0 || userProgress.CompletedQuestions > userProgress.TotalQuestions)
                return Result.Failure("Số câu hỏi đã hoàn thành không hợp lệ.");

            if (userProgress.BestResults < 0 || userProgress.BestResults > userProgress.TotalQuestions)
                return Result.Failure("Kết quả tốt nhất không hợp lệ.");

            if (userProgress.FirstAttemptAt > userProgress.LastAttemptAt)
                return Result.Failure("Lần thử đầu tiên không được sau lần thử cuối cùng.");

            if (userProgress.CompletedAt < userProgress.LastAttemptAt && userProgress.CompletedAt != DateTime.MinValue)
                return Result.Failure("Ngày hoàn thành không được trước lần thử cuối.");

            // Kiểm tra User và TestSet có tồn tại không (giả sử bạn có repository cho User và TestSet)
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userProgress.UserId);
            if (user == null)
                return Result.Failure("Người dùng không tồn tại.");

            var testSet = await _unitOfWork.TestSetRepository.GetByIdAsync(userProgress.TestSetId);
            if (testSet == null)
                return Result.Failure("TestSet không tồn tại.");

            await _unitOfWork.UserProgressRepository.Add(userProgress);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success("Tiến độ người dùng đã được tạo thành công.");
        }


        public async Task<PageResultDto<UserProgressDto>> GetAllUserProgressPagingAsync(int page, int pageSize)
        {


            var query = _unitOfWork.UserProgressRepository.GetAllForPaging()
                .Include(rq => rq.User)
                .Include(rq => rq.TestSet);

            var totalItem = await query.CountAsync();

            var userProgresses = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultDto<UserProgressDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItem = totalItem,
                Items = _mapper.Map<List<UserProgressDto>>(userProgresses)
            };
        }


        public async Task<UserProgress> GetUserProgressBestResultByTestSetIdAsync(int testSetid, Guid userId)
        {
            var userProgress = await _unitOfWork.UserProgressRepository.GetAll()
                .Where(up => up.TestSetId == testSetid && up.UserId == userId)
                .Include(up => up.User)
                .Include(up => up.TestSet)
                .ToListAsync();

            // Trong các bản ghi sớm nhất, tìm BestResults cao nhất
            var bestResult = userProgress
                .OrderByDescending(up => up.BestResults)
                .FirstOrDefault();

            if (bestResult == null)
                return null;

            return bestResult;
        }

        public async Task<bool> UpdateAllBestScore(int bestScore, int testSetId, Guid userId)
        {
            var ups = await _unitOfWork.UserProgressRepository.GetAll()
                .Where(up => up.TestSetId == testSetId && up.UserId == userId)
                .Where(up => up.BestResults < bestScore)
                .ToListAsync();

            if (ups == null || ups.Count == 0)
                return false;

            else
            {
                foreach (var up in ups)
                {
                    up.BestResults = bestScore;
                    await _unitOfWork.UserProgressRepository.Update(up);
                }
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
        }

    }
}
