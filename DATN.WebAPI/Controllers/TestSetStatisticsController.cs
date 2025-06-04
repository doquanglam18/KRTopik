using DATN.Application.Dtos.StatisticsDtos;
using DATN.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DATN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
/*    [Authorize("AdminOnly")]*/
    public class TestSetStatisticsController : ControllerBase
    {
        private readonly ITestSetStatisticsService _statisticsService;

        public TestSetStatisticsController(ITestSetStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpPost("statistics")]
        public async Task<ActionResult<List<TestSetStatisticsDto>>> GetStatistics([FromBody] TestSetStatisticsRequestDto request)
        {
            try
            {
                var statistics = await _statisticsService.GetTestSetStatisticsAsync(request);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("statistics/rank/{rankId}")]
        public async Task<ActionResult<List<TestSetStatisticsDto>>> GetStatisticsByRank(int rankId, [FromBody] TestSetStatisticsRequestDto request)
        {
            try
            {
                var statistics = await _statisticsService.GetTestSetStatisticsByRankAsync(rankId, request);
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
} 