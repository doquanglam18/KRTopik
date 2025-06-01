using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Dtos.TestSetDtos.ForAdmin;
using DATN.Application.Dtos.UserProgressDtos;
using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Interfaces
{
    public interface IUserProgressService
    {

        Task<Result> CreateUserProgressAsync(UserProgress userProgress);
        Task<PageResultDto<UserProgressDto>> GetAllUserProgressPagingAsync(int page, int pageSize);

        Task<UserProgress> GetUserProgressBestResultByTestSetIdAsync(int testSetid, Guid userId);

        Task<bool> UpdateAllBestScore(int bestScore, int testSetId, Guid userId);
    }
}
