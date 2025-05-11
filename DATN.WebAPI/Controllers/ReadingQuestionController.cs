using AutoMapper;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Dtos.UserDtos;
using DATN.Application.Services;
using DATN.Application.Services.Implements;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingQuestionController : ControllerBase
    {
        private readonly IReadingQuestionService _readingQuestionService;
        private readonly IMapper _mapper;
        private readonly ISystemLoggingService _loggingService;
        private readonly ICloudService _cloudService;
        public ReadingQuestionController(ICloudService cloudService ,IReadingQuestionService readingQuestionService, IMapper mapper, ISystemLoggingService systemLoggingService)
        {
            _readingQuestionService = readingQuestionService;
            _mapper = mapper;
            _loggingService = systemLoggingService;
            _cloudService = cloudService;
        }

/*        [Authorize("SystemAdminOnly")]
        [Authorize("AdminOnly")]*/
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllReadingQuestion()
        {
            var readingQuestions = await _readingQuestionService.GetAllReadingQuestionsAsync();
            var questions = _mapper.Map<IEnumerable<ReadingQuestionDto>>(readingQuestions);
            return Ok(questions);
        }


        [HttpPost("add")]
        public async Task<IActionResult> CreateReadingQuestion([FromForm] ReadingQuestionCreateDto createDto)
        {
            // Nếu có ảnh thì upload lên Cloudinary và gán URL
            if (createDto.Image != null)
            {
                var imageUrl = await _cloudService.UploadImageAsync(createDto.Image);
                if (string.IsNullOrEmpty(imageUrl))
                    return BadRequest("Tải ảnh thất bại.");

                createDto.ReadingImageURL = imageUrl; // Gán URL vào DTO
            }

            createDto.CreatedBy = ClaimsPrincipalExtensions.GetUserId(User);

            var readingQuestion = _mapper.Map<ReadingQuestion>(createDto);
            var result = await _readingQuestionService.CreateReadingQuestionAsync(readingQuestion);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateReadingQuestion(int id, [FromForm] ReadingQuestionDto updateDto)
        {
            var existingQuestion = await _readingQuestionService.GetReadingQuestionByIdAsync(id);
            if (existingQuestion == null)
                return NotFound("Không tìm thấy câu hỏi.");

            // Upload ảnh nếu có
            if (updateDto.Image != null)
            {
                var imageUrl = await _cloudService.UploadImageAsync(updateDto.Image);
                if (string.IsNullOrEmpty(imageUrl))
                    return BadRequest("Tải ảnh thất bại.");

                updateDto.ReadingImageURL = imageUrl;
            }

            updateDto.UpdatedBy = ClaimsPrincipalExtensions.GetUserId(User);

            // Map từ DTO lên entity (câu hỏi)
            var updatedQuestion = _mapper.Map<ReadingQuestion>(updateDto);

            // Cập nhật trường IsPublic sau khi ánh xạ
            existingQuestion.IsPublic = updateDto.IsPublic;

            // Cập nhật đáp án nếu có sự thay đổi trong DTO
            var updatedAnswers = updateDto.ReadingAnswers;

            // Xóa các đáp án cũ không có trong danh sách mới
            foreach (var existingAnswer in existingQuestion.ReadingAnswers.ToList())
            {
                if (!updatedAnswers.Any(a => a.Id == existingAnswer.Id))
                {
                    existingQuestion.ReadingAnswers.Remove(existingAnswer);
                }
            }

            // Thêm hoặc cập nhật các đáp án mới
            foreach (var updatedAnswer in updatedAnswers)
            {
                var existingAnswer = existingQuestion.ReadingAnswers
                    .FirstOrDefault(a => a.Id == updatedAnswer.Id);

                if (existingAnswer != null)
                {
                    // Cập nhật đáp án cũ
                    existingAnswer.Content = updatedAnswer.Content;
                    existingAnswer.IsCorrect = updatedAnswer.IsCorrect;
                }
                else
                {
                    // Thêm đáp án mới
                    existingQuestion.ReadingAnswers.Add(new ReadingAnswer
                    {
                        Content = updatedAnswer.Content,
                        IsCorrect = updatedAnswer.IsCorrect,
                        ReadingQuestion = existingQuestion // liên kết lại với câu hỏi
                    });
                }
            }

            // Cập nhật câu hỏi và lưu
            var result = await _readingQuestionService.UpdateReadingQuestionAsync(existingQuestion);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReadingQuestion(int id)
        {
            var result = await _readingQuestionService.DeleteReadingQuestionAsync(id);

            if (result.IsSuccess)
            {
                return Ok(new
                {
                    success = true,
                    message = result.Message
                });
            }

            return BadRequest(new
            {
                success = false,
                message = result.Message
            });
        }



    }
}
