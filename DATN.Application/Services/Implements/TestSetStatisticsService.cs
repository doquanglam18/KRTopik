using DATN.Application.Dtos.StatisticsDtos;
using DATN.Application.Services.Interfaces;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
    public class TestSetStatisticsService : ITestSetStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestSetStatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<List<TestSetStatisticsDto>> GetTestSetStatisticsAsync(TestSetStatisticsRequestDto request)
        {
            var data = await _unitOfWork.UserProgressRepository.GetAll()
                .Include(up => up.TestSet)
                    .ThenInclude(ts => ts.RankQuestion)
                .Where(up => up.LastAttemptAt >= request.StartDate && up.LastAttemptAt <= request.EndDate)
                .ToListAsync();

            var statistics = data
                .GroupBy(up =>
                {
                    DateTime dt = up.LastAttemptAt.Date;
                    DateTime dateKey = request.TimeUnit switch
                    {
                        StatisticsTimeUnit.Day => dt,
                        StatisticsTimeUnit.Week => dt.AddDays(-(int)dt.DayOfWeek), // ??u tu?n (Ch? nh?t = 0)
                        StatisticsTimeUnit.Month => new DateTime(dt.Year, dt.Month, 1),
                        StatisticsTimeUnit.Year => new DateTime(dt.Year, 1, 1),
                        _ => dt
                    };

                    return new
                    {
                        RankId = up.TestSet.RankQuestionId,
                        RankName = up.TestSet.RankQuestion.RankQuestionName,
                        Date = dateKey
                    };
                })
                .Select(g => new TestSetStatisticsDto
                {
                    RankQuestionId = g.Key.RankId,
                    RankQuestionName = g.Key.RankName,
                    TotalAttempts = g.Count(),
                    TotalUsers = g.Select(x => x.UserId).Distinct().Count(),
                    AverageScore = g.Average(x => x.BestResults),
                    Date = g.Key.Date
                })
                .OrderBy(x => x.Date)
                .ThenBy(x => x.RankQuestionId)
                .ToList();

            return statistics;
        }


        /*   public async Task<List<TestSetStatisticsDto>> GetTestSetStatisticsAsync(TestSetStatisticsRequestDto request)
           {
               var query = _unitOfWork.UserProgressRepository.GetAll()
                   .Include(up => up.TestSet)
                       .ThenInclude(ts => ts.RankQuestion)
                   .Where(up => up.LastAttemptAt >= request.StartDate && up.LastAttemptAt <= request.EndDate);

               var statistics = await query
                   .GroupBy(up => new
                   {
                       RankId = up.TestSet.RankQuestionId,
                       RankName = up.TestSet.RankQuestion.RankQuestionName,
                       Date = request.TimeUnit == StatisticsTimeUnit.Day ? 
                           EF.Functions.DateFromParts(up.LastAttemptAt.Year, up.LastAttemptAt.Month, up.LastAttemptAt.Day) :
                           request.TimeUnit == StatisticsTimeUnit.Week ?
                               EF.Functions.DateFromParts(up.LastAttemptAt.Year, up.LastAttemptAt.Month, 1).AddDays(-(int)up.LastAttemptAt.DayOfWeek) :
                               request.TimeUnit == StatisticsTimeUnit.Month ?
                                   EF.Functions.DateFromParts(up.LastAttemptAt.Year, up.LastAttemptAt.Month, 1) :
                                   EF.Functions.DateFromParts(up.LastAttemptAt.Year, 1, 1)
                   })
                   .Select(g => new TestSetStatisticsDto
                   {
                       RankQuestionId = g.Key.RankId,
                       RankQuestionName = g.Key.RankName,
                       TotalAttempts = g.Count(),
                       TotalUsers = g.Select(x => x.UserId).Distinct().Count(),
                       AverageScore = g.Average(x => x.BestResults),
                       Date = g.Key.Date
                   })
                   .OrderBy(x => x.Date)
                   .ThenBy(x => x.RankQuestionId)
                   .ToListAsync();

               return statistics;
           }*/

        /* public async Task<List<TestSetStatisticsDto>> GetTestSetStatisticsByRankAsync(int rankId, TestSetStatisticsRequestDto request)
         {
             var query = _unitOfWork.UserProgressRepository.GetAll()
                 .Include(up => up.TestSet)
                     .ThenInclude(ts => ts.RankQuestion)
                 .Where(up => up.TestSet.RankQuestionId == rankId)
                 .Where(up => up.LastAttemptAt >= request.StartDate && up.LastAttemptAt <= request.EndDate);

             var statistics = await query
                 .GroupBy(up => new
                 {
                     Date = request.TimeUnit == StatisticsTimeUnit.Day ? 
                         EF.Functions.DateFromParts(up.LastAttemptAt.Year, up.LastAttemptAt.Month, up.LastAttemptAt.Day) :
                         request.TimeUnit == StatisticsTimeUnit.Week ?
                             EF.Functions.DateFromParts(up.LastAttemptAt.Year, up.LastAttemptAt.Month, 1).AddDays(-(int)up.LastAttemptAt.DayOfWeek) :
                             request.TimeUnit == StatisticsTimeUnit.Month ?
                                 EF.Functions.DateFromParts(up.LastAttemptAt.Year, up.LastAttemptAt.Month, 1) :
                                 EF.Functions.DateFromParts(up.LastAttemptAt.Year, 1, 1)
                 })
                 .Select(g => new TestSetStatisticsDto
                 {
                     RankQuestionId = rankId,
                     RankQuestionName = g.First().TestSet.RankQuestion.RankQuestionName,
                     TotalAttempts = g.Count(),
                     TotalUsers = g.Select(x => x.UserId).Distinct().Count(),
                     AverageScore = g.Average(x => x.BestResults),
                     Date = g.Key.Date
                 })
                 .OrderBy(x => x.Date)
                 .ToListAsync();

             return statistics;
         }*/

        public async Task<List<TestSetStatisticsDto>> GetTestSetStatisticsByRankAsync(int rankId, TestSetStatisticsRequestDto request)
        {
            var data = await _unitOfWork.UserProgressRepository.GetAll()
                .Include(up => up.TestSet)
                    .ThenInclude(ts => ts.RankQuestion)
                .Where(up => up.TestSet.RankQuestionId == rankId)
                .Where(up => up.LastAttemptAt >= request.StartDate && up.LastAttemptAt <= request.EndDate)
                .ToListAsync();

            var grouped = data.GroupBy(up =>
            {
                DateTime dt = up.LastAttemptAt.Date;

                return request.TimeUnit switch
                {
                    StatisticsTimeUnit.Day => dt,
                    StatisticsTimeUnit.Week => dt.AddDays(-(int)dt.DayOfWeek),
                    StatisticsTimeUnit.Month => new DateTime(dt.Year, dt.Month, 1),
                    StatisticsTimeUnit.Year => new DateTime(dt.Year, 1, 1),
                    _ => dt
                };
            });

            var statistics = grouped.Select(g => new TestSetStatisticsDto
            {
                RankQuestionId = rankId,
                RankQuestionName = g.First().TestSet.RankQuestion.RankQuestionName,
                TotalAttempts = g.Count(),
                TotalUsers = g.Select(x => x.UserId).Distinct().Count(),
                AverageScore = g.Average(x => x.BestResults),
                Date = g.Key
            })
            .OrderBy(x => x.Date)
            .ToList();

            return statistics;
        }

    }
} 