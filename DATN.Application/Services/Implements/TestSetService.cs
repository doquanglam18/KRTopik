using AutoMapper;
using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.TestSetDtos;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
    public class TestSetService : ITestSetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TestSetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> CreateTestSetAsync(TestSet testSet)
        {
            // 1. Validate đầu vào
            if (testSet.RankQuestionId <= 0)
            {
                return Result.Failure("Mức độ đề không hợp lệ !");
            }

            // 2. Gán giá trị mặc định
            testSet.CreatedDate = DateTime.UtcNow;
            testSet.UpdatedDate = DateTime.UtcNow;
            testSet.IsDelele = false;

            // Đảm bảo không null danh sách
            testSet.ListeningQuestions ??= new List<ListeningQuestion>();
            testSet.ReadingQuestions ??= new List<ReadingQuestion>();
            testSet.Comments ??= new List<Comment>();
            testSet.UserProgress ??= new List<UserProgress>();

            try
            {
                // 3. Thêm vào context và lưu
                await _unitOfWork.TestSetRepository.Add(testSet);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Thêm đề thành công !");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return Result.Failure($"Có lỗi khi tạo đề: {ex.Message}");
            }
        }


        public Task<Result> DeleteTestSetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TestSet>> GetAllTestSetAsync()
        {
            var testSets = await _unitOfWork.TestSetRepository
               .GetAll()
               .Where(rq => rq.IsDelele == false)
               .ToListAsync();
            return testSets; 
        }

        public async Task<PageResultDto<TestSetForUserDto>> GetAllTestSetPagingAsync(int page, int pageSize)
        {
            var query = _unitOfWork.TestSetRepository.GetAllForPaging()
              .Include(rq => rq.RankQuestion)
                .Include(rq => rq.ReadingQuestions)
                .Include(rq => rq.UserProgress)
                .Include(rq => rq.ListeningQuestions)
                .Include(rq => rq.Comments)
                .Where(rq => rq.IsDelele == false);
            var totalItem = query.Count();

            var testSets = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PageResultDto<TestSetForUserDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItem = totalItem,
                Items = _mapper.Map<List<TestSetForUserDto>>(testSets)
            };
        }

        public async Task<TestSet> GetTestSetByIdAsync(int id)
        {
            var testSet = await _unitOfWork.TestSetRepository.GetByIdAsync(id);
            return testSet;
        }


        public async Task<PageResultDto<TestSetForUserDto>> GetAllTestSetPagingByRankAsync(int page, int pageSize, int rankId)
        {
            if (rankId <= 0)
            {
                return new PageResultDto<TestSetForUserDto>
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalItem = 0,
                    Items = new List<TestSetForUserDto>()
                };
            }

            var query = _unitOfWork.TestSetRepository.GetAllForPaging()
                .Include(rq => rq.RankQuestion)
                .Include(rq => rq.ReadingQuestions)
                .Include(rq => rq.UserProgress)
                .Include(rq => rq.ListeningQuestions)
                .Include(rq => rq.Comments)
                .Where(rq => rq.IsDelele == false)
                .Where(rq => rq.RankQuestionId == rankId);

            var totalItem = await query.CountAsync();

            var testSets = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResultDto<TestSetForUserDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItem = totalItem,
                Items = _mapper.Map<List<TestSetForUserDto>>(testSets)
            };
        }


        public Task<Result> UpdateTestSetAsync(TestSet testSet)
        {
            throw new NotImplementedException();
        }
    }
}
