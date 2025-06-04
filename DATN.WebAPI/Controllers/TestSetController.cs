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
using DATN.Application.Dtos.TestSetDtos.ForAdmin;
using DATN.WebAPI.Extensions;
using System.Security.Claims;
using System.Linq;

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
        private readonly IUserProgressService _userProgressService;
        public TestSetController(ICloudService cloudService, ITestSetService testSetService, IMapper mapper, ISystemLoggingService systemLoggingService, IUserProgressService userProgressService)
        {
            _testSetService = testSetService;
            _mapper = mapper;
            _loggingService = systemLoggingService;
            _cloudService = cloudService;
            _userProgressService = userProgressService;
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


        [HttpGet("forpaggingadmin/{page}/{pageSize}")]
        public async Task<ActionResult> GetTestSetForAdminPaged([FromRoute] int page, [FromRoute] int pageSize)
        {
            var testSets = await _testSetService.GetAllTestSetForAdminPagingAsync(page, pageSize);
            return Ok(testSets);
        }

        [HttpGet("getByRankadmin/{rankId}/{page}/{pageSize}")]
        public async Task<ActionResult> GetListTestSetForAdminPagedByRank([FromRoute] int page, [FromRoute] int pageSize, [FromRoute] int rankId)
        {
            var testSets = await _testSetService.GetAllTestSetForAdminPagingByRankAsync(page, pageSize, rankId);
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

        [HttpGet("getByIdForAdmin/{id}")]
        public async Task<IActionResult> GetTestSetForAdminById(int id)
        {
            var testSet = await _testSetService.GetTestSetByIdAsync(id);
            if (testSet == null)
                return NotFound("Không tìm thấy bộ đề thi với ID này.");

            var question = _mapper.Map<TestSetDetailsForAdmin>(testSet);
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




        [HttpPost("scoring")]
        public async Task<IActionResult> ScoringTestSet(SubmitTestDto submitTestDto)
        {
            if (submitTestDto == null)
                return BadRequest("Dữ liệu không hợp lệ.");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy UserId trực tiếp từ ClaimsPrincipal
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("Người dùng chưa đăng nhập.");
            }
            var userId = Guid.Parse(userIdClaim.Value);

            var result = await _testSetService.ScoringTestSetAsync(submitTestDto);
           
            var bestResultUP = await _userProgressService.GetUserProgressBestResultByTestSetIdAsync(submitTestDto.TestSetId, userId);


            // Lấy thông tin test set để biết tổng số câu hỏi
            var testSet = await _testSetService.GetTestSetByIdAsync(submitTestDto.TestSetId);
            if (testSet != null)
            {
                var now = DateTime.UtcNow;
                // Tạo tiến trình mới cho lần làm đề này
                var userProgress = new UserProgress
                {
                    UserId = userId,
                    TestSetId = submitTestDto.TestSetId,
                    TotalQuestions = result.TotalQuestions,
                    CompletedQuestions = result.CorrectCount,
                    BestResults = result.CorrectCount,
                    FirstAttemptAt = submitTestDto.FirstAttemptAt,
                    LastAttemptAt = submitTestDto.LastAttemptAt,
                    CompletedAt = submitTestDto.Answers.Count == result.CorrectCount ? submitTestDto.LastAttemptAt : DateTime.MinValue
                };
                
                if(bestResultUP != null)
                {
                    if (userProgress.BestResults > bestResultUP.BestResults)
                    {
                        await _userProgressService.UpdateAllBestScore(userProgress.BestResults, submitTestDto.TestSetId, userId);
                    }
                    else
                    {
                        userProgress.BestResults = bestResultUP.BestResults;
                    }
                }
                
                await _userProgressService.CreateUserProgressAsync(userProgress);
            }

            return Ok(result);
        }
    

        [HttpPost("updateQuestions")]
        public async Task<IActionResult> UpdateTestSetQuestions([FromBody] UpdateTestSetQuestionsRequest request)
        {
            try
            {
                if (request == null || request.QuestionIds == null)
                {
                    return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });
                }

                // Lấy testset hiện tại
                var testSet = await _testSetService.GetTestSetByIdAsync(request.TestSetId);
                if (testSet == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy đề thi." });
                }

                // Xác định loại câu hỏi dựa vào RankQuestionId
                if ((request.RankQuestionId >= 1 && request.RankQuestionId <= 12) || request.RankQuestionId == 21)
                {
                    // Xử lý câu hỏi Reading
                    var result = await _testSetService.UpdateReadingQuestionsAsync(request.TestSetId, request.QuestionIds);
                    if (!result.IsSuccess)
                    {
                        return BadRequest(new { success = false, message = result.Message });
                    }
                }
                else if (request.RankQuestionId >= 13 && request.RankQuestionId <= 20)
                {
                    // Xử lý câu hỏi Listening
                    var result = await _testSetService.UpdateListeningQuestionsAsync(request.TestSetId, request.QuestionIds);
                    if (!result.IsSuccess)
                    {
                        return BadRequest(new { success = false, message = result.Message });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Loại câu hỏi không hợp lệ." });
                }

                return Ok(new { success = true, message = "Cập nhật câu hỏi thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Có lỗi xảy ra: {ex.Message}" });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTestSet([FromBody] TestSetForCreateDto testSetForCreateDto)
        {
            if (testSetForCreateDto == null)
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });

            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ.", errors = ModelState.Values.SelectMany(v => v.Errors) });
            }

            // Validate QuestionIds
            if (testSetForCreateDto.QuestionIds == null || !testSetForCreateDto.QuestionIds.Any())
            {
                return BadRequest(new { success = false, message = "Danh sách câu hỏi không được để trống." });
            }

            // Lấy UserId trực tiếp từ ClaimsPrincipal
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { success = false, message = "Người dùng chưa đăng nhập." });
            }
            var userId = Guid.Parse(userIdClaim.Value);

            testSetForCreateDto.CreatedBy = userId;

            try 
            {
                var testSet = _mapper.Map<TestSet>(testSetForCreateDto);
                var resultV = await _testSetService.CreateTestSetAsync(testSet);

                if (!resultV.IsSuccess)
                    return BadRequest(new { success = false, message = resultV.Message });

                Result updateResult = null;
                // Xác định loại câu hỏi dựa vào RankQuestionId
                if ((testSet.RankQuestionId >= 1 && testSet.RankQuestionId <= 12) || testSet.RankQuestionId == 21)
                {
                    // Xử lý câu hỏi Reading
                    updateResult = await _testSetService.UpdateReadingQuestionsAsync(resultV.Data, testSetForCreateDto.QuestionIds);
                }
                else if (testSet.RankQuestionId >= 13 && testSet.RankQuestionId <= 20)
                {
                    // Xử lý câu hỏi Listening
                    updateResult = await _testSetService.UpdateListeningQuestionsAsync(resultV.Data, testSetForCreateDto.QuestionIds);
                }
                else
                {
                    // Nếu loại câu hỏi không hợp lệ, xóa TestSet vừa tạo
                    await _testSetService.DeleteTestSetAsync(resultV.Data);
                    return BadRequest(new { success = false, message = "Loại câu hỏi không hợp lệ." });
                }

                if (!updateResult.IsSuccess)
                {
                    // Nếu cập nhật câu hỏi thất bại, xóa TestSet vừa tạo
                    await _testSetService.DeleteTestSetAsync(resultV.Data);
                    return BadRequest(new { success = false, message = updateResult.Message });
                }

                return Ok(new { 
                    success = true, 
                    message = "Tạo đề thành công !",
                });
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return StatusCode(500, new { 
                    success = false, 
                    message = "Có lỗi xảy ra khi tạo đề thi.",
                    error = ex.Message 
                });
            }
        }



        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTestSet(int id)
        {
            var result = await _testSetService.DeleteTestSetAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(new { success = true, message = result.Message });
        }



    }
}
