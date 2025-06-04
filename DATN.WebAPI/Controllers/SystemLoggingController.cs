using AutoMapper;
using CloudinaryDotNet.Actions;
using DATN.Application.Dtos.RoleDtos;
using DATN.Application.Dtos.SystemLoggingDtos;
using DATN.Application.Services;
using DATN.Application.Services.Implements;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemLoggingController : ControllerBase
    {
        private readonly ISystemLoggingService _systemLogging;
        private readonly IMapper _mapper;
        private readonly ISystemLoggingService _loggingService;
        private readonly ILogger<SystemLoggingController> _logger;
        public SystemLoggingController(ISystemLoggingService systemLogging, IMapper mapper, ISystemLoggingService systemLoggingService, ILogger<SystemLoggingController> logger)
        {
            _systemLogging = systemLogging;
            _mapper = mapper;
            _loggingService = systemLoggingService;
            _logger = logger;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllSystemLogging()
        {
            var systemloggings = await _systemLogging.GetAllSystemLoggingAsync();
            var systemloggingsDto = _mapper.Map<IEnumerable<SystemLoggingDto>>(systemloggings);
            return Ok(systemloggingsDto);
        }


        [HttpPost("loggingaction")]
        public async Task<IActionResult> LoggingAction([FromBody] Dictionary<string, string> data)
        {
            _logger.LogInformation("Received logging data: {@data}", data);

            try
            {
                data.TryGetValue("actionVName", out var actionVName);
                data.TryGetValue("details", out var details);


                var userId = User?.GetUserId() ?? null;
                
                // Nếu userId là Guid.Empty, set thành null để tránh lỗi foreign key
                if (userId == Guid.Empty)
                {
                    userId = null;
                }

                var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown IP";

                await _loggingService.LogAction(userId, ip, actionVName, details);

                _logger.LogInformation("Ghi log: {Action} - {Details} - User: {UserId} - IP: {IP}", actionVName, details, userId, ip);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi ghi log hành động");
                return StatusCode(500, "Lỗi khi ghi log hành động");
            }
        }



        [HttpGet("stats")]
        public async Task<IActionResult> GetStats(DateTime? from, DateTime? to, string? action, string? ip, Guid? userId)
        {
            _logger.LogInformation($"Getting stats with params: from={from}, to={to}, action={action}, ip={ip}, userId={userId}");
            var stats = await _loggingService.GetAccessStatsAsync(from, to, action, ip, userId);
            _logger.LogInformation($"Found {stats?.Count() ?? 0} stats records");
            return Ok(stats);
        }

        [HttpGet("chart")]
        public async Task<IActionResult> GetChart(DateTime? from, DateTime? to)
        {
            _logger.LogInformation($"Getting chart data with params: from={from}, to={to}");
            var chartData = await _loggingService.GetDailyAccessChartAsync(from, to);
            _logger.LogInformation($"Found {chartData?.Count() ?? 0} chart data records");
            return Ok(chartData);
        }





    }
}
