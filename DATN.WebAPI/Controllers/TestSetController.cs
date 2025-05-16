using AutoMapper;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Services.Implements;
using DATN.Application.Services;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.Application.Dtos.TestSetDtos;
using DATN.Application.Dtos.BaseDtos;
using System.Drawing.Printing;

namespace DATN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSetController : ControllerBase
    {
        private readonly ITestSetService _testSetService;
        private readonly IMapper _mapper;
        private readonly ISystemLoggingService _loggingService;
        private readonly ICloudService _cloudService;
        public TestSetController(ICloudService cloudService, ITestSetService testSetService, IMapper mapper, ISystemLoggingService systemLoggingService)
        {
            _testSetService = testSetService;
            _mapper = mapper;
            _loggingService = systemLoggingService;
            _cloudService = cloudService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllTestSet()
        {
            var testSets = await _testSetService.GetAllTestSetAsync();
            var questions = _mapper.Map<IEnumerable<TestSetForUserDto>>(testSets);
            return Ok(questions);
        }

        [HttpGet("forpagging/{page}/{pageSize}")]
        public async Task<ActionResult> GetTestSetPaged([FromRoute] int page, [FromRoute] int pageSize)
        {
            var testSets = await _testSetService.GetAllTestSetPagingAsync(page, pageSize);
            return Ok(testSets);
        }

        [HttpGet("getByRank/{rankId}/{page}/{pageSize}")]
        public async Task<ActionResult> GetListTestSetPagedByRank([FromRoute] int page, [FromRoute] int pageSize, [FromRoute] int rankId)
        {
            var testSets = await _testSetService.GetAllTestSetPagingByRankAsync(page, pageSize, rankId);
            return Ok(testSets);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetTestSetById(int id)
        {
            var testSet = await _testSetService.GetTestSetByIdAsync(id);
            if (testSet == null)
                return NotFound("Không tìm thấy bộ đề thi với ID này.");

            var question = _mapper.Map<TestSetDetailsDto>(testSet);
            return Ok(question);
        }

        [HttpGet("getDoById/{id}")]
        public async Task<IActionResult> GetDoTestSetById(int id)
        {
            var testSet = await _testSetService.GetTestSetByIdAsync(id);
            if (testSet == null)
                return NotFound("Không tìm thấy bộ đề thi với ID này.");

            var question = _mapper.Map<DoTestSetDto>(testSet);
            return Ok(question);
        }


        [HttpGet("getAudioDuration")]
        public async Task<IActionResult> GetAudioDuration(string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("URL không hợp lệ.");

            string publicId = ClaimsPrincipalExtensions.ExtractPublicIdFromUrl(url);

            var duration = await _cloudService.GetAudioDurationViaApiAsync(publicId);
            if (duration == null)
                return BadRequest("Không thể lấy thời gian của audio.");

            return Ok(new { Duration = duration });
        }
        /*   [HttpPost("add")]
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
           }*/


        /*        [HttpDelete("{id}")]
                public Task<IActionResult> DeleteTestSet(int id)
                {
        *//*            var result = await _listeningQuestionService.DeleteListeningQuestionAsync(id);

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
                    });*//*
                }*/

        /*   [HttpPut("update/{id}")]
           public async Task<IActionResult> UpdateListeningQuestion(int id, [FromForm] ListeningQuestionDto updateDto)
           {

           }

           [HttpGet("forpagging/{page}/{pageSize}")]
           public async Task<ActionResult> GetListeningQuestionPaged([FromRoute] int page, [FromRoute] int pageSize)
           {
               var listeningQuestions = await _listeningQuestionService.GetAllListeningQuestionsPagingAsync(page, pageSize);
               return Ok(listeningQuestions);
           }*/


    }
}
