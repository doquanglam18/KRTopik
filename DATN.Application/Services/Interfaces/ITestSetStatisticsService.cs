using DATN.Application.Dtos.StatisticsDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DATN.Application.Services.Interfaces
{
    public interface ITestSetStatisticsService
    {
        Task<List<TestSetStatisticsDto>> GetTestSetStatisticsAsync(TestSetStatisticsRequestDto request);
        Task<List<TestSetStatisticsDto>> GetTestSetStatisticsByRankAsync(int rankId, TestSetStatisticsRequestDto request);
    }
} 