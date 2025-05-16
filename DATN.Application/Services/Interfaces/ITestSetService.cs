using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.TestSetDtos;
using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Interfaces
{
    public interface ITestSetService
    {
        Task<IEnumerable<TestSet>> GetAllTestSetAsync();
        Task<TestSet> GetTestSetByIdAsync(int id);
        Task<Result> CreateTestSetAsync(TestSet testSet);
        Task<Result> UpdateTestSetAsync(TestSet testSet);
        Task<Result> DeleteTestSetAsync(int id);
        Task<PageResultDto<TestSetForUserDto>> GetAllTestSetPagingAsync(int page, int pageSize);

        Task<PageResultDto<TestSetForUserDto>> GetAllTestSetPagingByRankAsync(int page, int pageSize, int rankId);
    }
}
