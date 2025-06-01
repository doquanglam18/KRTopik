using AutoMapper;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Dtos.UserProgressDtos;
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
    public class UserProgressController : ControllerBase
    {
        private readonly IUserProgressService _userProgressService;
        private readonly IMapper _mapper;
        public UserProgressController(IUserProgressService userProgressService, IMapper mapper)
        {
            _mapper = mapper;
            _userProgressService = userProgressService;
        }


        [HttpGet("forpaggingUserProgress/{page}/{pageSize}")]
        public async Task<ActionResult> GetUserProgressPaged([FromRoute] int page, [FromRoute] int pageSize)
        {
            var userProgreses = await _userProgressService.GetAllUserProgressPagingAsync(page, pageSize);
            return Ok(userProgreses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReadingQuestion([FromForm] CreateUserProgressDto createDto)
        {
            // Validate ModelState nếu bạn dùng Data Annotations (tuỳ chọn)
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { message = "Dữ liệu không hợp lệ", errors });
            }

            var createUserProgress = _mapper.Map<UserProgress>(createDto);

            // Gọi service xử lý nghiệp vụ
            var result = await _userProgressService.CreateUserProgressAsync(createUserProgress);

            // Xử lý phản hồi từ service
            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(new { message = "Tạo tiến độ người dùng thành công." });
        }

    }
}
