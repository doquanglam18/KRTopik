using AutoMapper;
using CloudinaryDotNet.Actions;
using DATN.Application.Dtos.KoreaBlogDtos;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Services;
using DATN.Application.Services.Implements;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Security.Claims;

namespace DATN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoreaBlogController : ControllerBase
    {
        private readonly IKoreaBlogService _koreaBlogService;
        private readonly IMapper _mapper;
        private readonly ISystemLoggingService _loggingService;
        private readonly ICloudService _cloudService;
        private readonly ILogger<KoreaBlogController> _logger;
        private readonly IWebHostEnvironment _environment;

        public KoreaBlogController(
            ICloudService cloudService,
            IKoreaBlogService koreaBlogService,
            IMapper mapper,
            ISystemLoggingService systemLoggingService,
            ILogger<KoreaBlogController> logger,
            IWebHostEnvironment environment)
        {
            _koreaBlogService = koreaBlogService;
            _mapper = mapper;
            _loggingService = systemLoggingService;
            _cloudService = cloudService;
            _logger = logger;
            _environment = environment;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllKoreaBlogQuestion()
        {
            var koreaBlogs = await _koreaBlogService.GetAllKoreaBlogAsync();
            var results = _mapper.Map<IEnumerable<KoreaBlogDto>>(koreaBlogs);
            return Ok(results);
        }

        [HttpGet("forpagging/{page}/{pageSize}")]
        public async Task<ActionResult> GetKoreaBlogPaged([FromRoute] int page, [FromRoute] int pageSize)
        {
            var koreaBlogs = await _koreaBlogService.GetAllKoreaBlogPagingAsync(page, pageSize);
            return Ok(koreaBlogs);
        }

        [HttpGet("forpaggingadmin/{page}/{pageSize}")]
        public async Task<ActionResult> GetKoreaBlogPagedForAdmin([FromRoute] int page, [FromRoute] int pageSize)
        {
            var koreaBlogs = await _koreaBlogService.GetAllKoreaBlogPagingAsyncForAdmin(page, pageSize);
            return Ok(koreaBlogs);
        }


        [HttpGet("search/{searchBlog}/{page}/{pageSize}")]
        public async Task<ActionResult> GetKoreaBlogForSearchPaged([FromRoute] string sreachName,[FromRoute] int page, [FromRoute] int pageSize)
        {
            var koreaBlogs = await _koreaBlogService.GetAllKoreaBlogForSearchPagingAsync( sreachName,page, pageSize);
            return Ok(koreaBlogs);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetKoreaBlogById([FromRoute] int id)
        {
            var koreaBlog = await _koreaBlogService.GetKoreaBlogByIdAsync(id);
            if (koreaBlog == null)
            {
                return NotFound("Không tìm thấy blog với ID đã cho");
            }

            var result = _mapper.Map<KoreaBlogDetailsDto>(koreaBlog);
            return Ok(result);
        }


        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> AddKoreaBlog([FromForm] KoreaBlogCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Bắt đầu thêm blog mới: {Title}", dto.Title);

                // Validate và upload ảnh lên Cloudinary nếu có
                if (dto.Image != null)
                {
                    // Kiểm tra kích thước file (giới hạn 5MB)
                    if (dto.Image.Length > 5 * 1024 * 1024)
                    {
                        return BadRequest("Kích thước file không được vượt quá 5MB");
                    }

                    // Kiểm tra định dạng file
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(dto.Image.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return BadRequest("Chỉ chấp nhận file ảnh (jpg, jpeg, png, gif)");
                    }

                    // Upload ảnh lên Cloudinary
                    var imageUrl = await _cloudService.UploadImageAsync(dto.Image);
                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        return BadRequest("Tải ảnh lên Cloudinary thất bại");
                    }

                    dto.BlogImageUrl = imageUrl;
                }

                // Lấy UserId trực tiếp từ ClaimsPrincipal
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("Người dùng chưa đăng nhập.");
                }
                var userId = Guid.Parse(userIdClaim.Value);

                // Set thông tin cần thiết trước khi map
                dto.CreateadBy = userId;
                dto.CreatedDate = DateTime.UtcNow;
                dto.View = 0;

                // Map DTO sang entity sử dụng AutoMapper
                var koreaBlog = _mapper.Map<KoreaBlog>(dto);

                // Gọi service để thêm blog
                var result = await _koreaBlogService.AddKoreaBlogAsync(koreaBlog);

                if (result.IsSuccess)
                {
                    _logger.LogInformation("Thêm blog thành công: {Title}", dto.Title);
                    return Ok(result);
                }

                _logger.LogWarning("Thêm blog thất bại: {Message}", result.Message);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi thêm blog: {Title}", dto.Title);
                return StatusCode(500, "Đã xảy ra lỗi khi thêm blog");
            }
        }
        [HttpPut("updateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id)
        {
            var koreaBlog = await _koreaBlogService.GetKoreaBlogByIdAsync(id);
            if (koreaBlog == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy blog với ID đã cho." });
            }

            try
            {
                koreaBlog.IsActive = !koreaBlog.IsActive;

                var result = await _koreaBlogService.UpdateKoreaBlogAsync(koreaBlog);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Cập nhật trạng thái blog thành công!");
                    return Ok(new { success = true, message = "Cập nhật trạng thái thành công!" });
                }
                else
                {
                    _logger.LogWarning("Cập nhật trạng thái blog thất bại! Lỗi: {Message}", result.Message);
                    return BadRequest(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật trạng thái blog với ID: {Id}", id);
                return StatusCode(500, new { success = false, message = "Lỗi máy chủ nội bộ." });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteKoreaBlog([FromRoute] int id)
        {
            var koreaBlog = await _koreaBlogService.GetKoreaBlogByIdAsync(id);
            if (koreaBlog == null)
            {
                return NotFound(new { success = false, message = "Không tìm thấy blog với ID đã cho." });
            }

            try
            {
                var result = await _koreaBlogService.DeleteKoreaBlogAsync(id);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Xóa blog thành công với ID: {Id}", id);
                    return Ok(new { success = true, message = "Xóa blog thành công!" });
                }
                else
                {
                    _logger.LogWarning("Xóa blog thất bại! Lỗi: {Message}", result.Message);
                    return BadRequest(new { success = false, message = result.Message });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa blog với ID: {Id}", id);
                return StatusCode(500, new { success = false, message = "Lỗi máy chủ nội bộ." });
            }
        }


        [HttpPut("updateView/{id}")]
        public async Task<IActionResult> UpdateViewBlog([FromRoute] int id)
        {
            var result = await _koreaBlogService.UpdateViewBlog(id);
            if (result.IsSuccess)
            {
                _logger.LogInformation("Cập nhật lượt xem blog thành công với ID: {Id}", id);
                return Ok(new { success = true, message = "Cập nhật lượt xem thành công!" });
            }
            else
            {
                _logger.LogWarning("Cập nhật lượt xem blog thất bại! Lỗi: {Message}", result.Message);
                return BadRequest(new { success = false, message = result.Message });
            }
        }

    }
}
