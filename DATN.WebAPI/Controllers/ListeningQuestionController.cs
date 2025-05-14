using AutoMapper;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Services;
using DATN.Application.Services.Implements;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DATN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListeningQuestionController : ControllerBase
    {
        private readonly IListeningQuestionService _listeningQuestionService;
        private readonly IMapper _mapper;
        private readonly ISystemLoggingService _loggingService;
        private readonly ICloudService _cloudService;
        public ListeningQuestionController(ICloudService cloudService, IListeningQuestionService listeningQuestionService, IMapper mapper, ISystemLoggingService systemLoggingService)
        {
            _listeningQuestionService = listeningQuestionService;
            _mapper = mapper;
            _loggingService = systemLoggingService;
            _cloudService = cloudService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllListeningQuestion()
        {
            var listeningQuestions = await _listeningQuestionService.GetAllListeningQuestionsAsync();
            var questions = _mapper.Map<IEnumerable<ListeningQuestionDto>>(listeningQuestions);
            return Ok(questions);
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateListeningQuestion([FromForm] ListeningQuestionCreateDto createDto)
        {
            if (createDto.Sound != null)
            {
                var soundUrl = await _cloudService.UploadAudioAsync(createDto.Sound);
                if (string.IsNullOrEmpty(soundUrl))
                    return BadRequest("Tải file nghe thất bại.");

                createDto.ListeningSoundURL = soundUrl; // Gán URL vào DTO
            }
            else
            {
                return BadRequest("Bắt buộc phải có file nghe cho câu hỏi nghe !");
            }

            // Upload ảnh từng đáp án (nếu có)
            foreach (var answer in createDto.ListeningAnswers)
            {
                if (answer.Image != null && answer.Image.Length > 0)
                {
                    var imageUrl = await _cloudService.UploadImageAsync(answer.Image);
                    if (string.IsNullOrEmpty(imageUrl))
                        return BadRequest("Tải ảnh đáp án thất bại.");

                    answer.Content = imageUrl; // Gán URL ảnh vào Content
                }
            }

            createDto.CreatedBy = ClaimsPrincipalExtensions.GetUserId(User);

            var listeningQuestion = _mapper.Map<ListeningQuestion>(createDto);
            var result = await _listeningQuestionService.CreateListeningQuestionAsync(listeningQuestion);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListeningQuestion(int id)
        {
            var result = await _listeningQuestionService.DeleteListeningQuestionAsync(id);

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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateListeningQuestion(int id, [FromForm] ListeningQuestionDto updateDto)
        {
            var existingQuestion = await _listeningQuestionService.GetListeningQuestionByIdAsync(id);
            if (existingQuestion == null)
                return NotFound("Không tìm thấy câu hỏi.");

            // Upload sound nếu có
            if (updateDto.Sound != null)
            {
                var soundUrl = await _cloudService.UploadAudioAsync(updateDto.Sound);
                if (string.IsNullOrEmpty(soundUrl))
                    return BadRequest("Tải file nghe thất bại.");

                updateDto.ListeningSoundURL = soundUrl;
            }

            // Upload ảnh từng đáp án (nếu có)
            if (updateDto.ListeningAnswers != null)
            {
                foreach (var answer in updateDto.ListeningAnswers)
                {
                    if (answer.Image != null && answer.Image.Length > 0)
                    {
                        var imageUrl = await _cloudService.UploadImageAsync(answer.Image);
                        if (string.IsNullOrEmpty(imageUrl))
                            return BadRequest("Tải ảnh đáp án thất bại.");

                        answer.Content = imageUrl; // Gán URL ảnh vào Content
                    }
                }
            }

            updateDto.UpdatedBy = ClaimsPrincipalExtensions.GetUserId(User);

            // Cập nhật thông tin câu hỏi
            existingQuestion.IsPublic = updateDto.IsPublic;
            existingQuestion.ListeningSoundURL = updateDto.ListeningSoundURL;
            existingQuestion.ListeningScript = updateDto.ListeningScript;


            // Cập nhật hoặc thêm đáp án mới
            var updatedAnswers = updateDto.ListeningAnswers;

            // Xóa các đáp án cũ không còn trong danh sách mới
            foreach (var existingAnswer in existingQuestion.ListeningAnswers.ToList())
            {
                if (!updatedAnswers.Any(a => a.Id == existingAnswer.Id))
                {
                    existingQuestion.ListeningAnswers.Remove(existingAnswer);
                }
            }

            // Thêm hoặc cập nhật các đáp án mới
            foreach (var updatedAnswer in updatedAnswers)
            {
                var existingAnswer = existingQuestion.ListeningAnswers
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
                    existingQuestion.ListeningAnswers.Add(new ListeningAnswer
                    {
                        Content = updatedAnswer.Content,
                        IsCorrect = updatedAnswer.IsCorrect,
                        ListeningQuestion = existingQuestion // liên kết lại với câu hỏi
                    });
                }
            }

            // Cập nhật câu hỏi và lưu
            var result = await _listeningQuestionService.UpdateListeningQuestionAsync(existingQuestion);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("forpagging/{page}/{pageSize}")]
        public async Task<ActionResult> GetListeningQuestionPaged([FromRoute] int page, [FromRoute] int pageSize)
        {
            var listeningQuestions = await _listeningQuestionService.GetAllListeningQuestionsPagingAsync(page, pageSize);
            return Ok(listeningQuestions);
        }


    }
}
