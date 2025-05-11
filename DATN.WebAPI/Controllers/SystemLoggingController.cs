using AutoMapper;
using DATN.Application.Dtos.RoleDtos;
using DATN.Application.Dtos.SystemLoggingDtos;
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
        public SystemLoggingController(ISystemLoggingService systemLogging, IMapper mapper, ISystemLoggingService systemLoggingService)
        {
            _systemLogging = systemLogging;
            _mapper = mapper;
            _loggingService = systemLoggingService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllSystemLogging()
        {
            var systemloggings = await _systemLogging.GetAllSystemLoggingAsync();
            var systemloggingsDto = _mapper.Map<IEnumerable<SystemLoggingDto>>(systemloggings);
            return Ok(systemloggingsDto);
        }
    }
}
