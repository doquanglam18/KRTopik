using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.TestSetDtos;
using DATN.Application.Dtos.TestSetDtos.ForAdmin;
using DATN.Application.Dtos.UserDtos;
using DATN.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Dynamic;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Dtos.ListeningDtos.ForAddTestSet;
using DATN.Application.Dtos.ReadingDtos.ForAddTestSet;
using Microsoft.AspNetCore.Authentication;
using System.Drawing.Printing;

namespace DATN.WebApp.Controllers
{
   

    public class TestSetController : Controller
    {

        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://localhost:7208/api/testset";

        public TestSetController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> ListTestSetForUser(int? rankQuesTionId, int page = 1, int pageSize = 5)
        {

            var rankQuestions = await _httpClient.GetFromJsonAsync<IEnumerable<RankQuestionDto>>("https://localhost:7208/api/rankquestion/getall");

            // Lưu vào ViewData
            ViewData["RankQuestions"] = rankQuestions;
            HttpResponseMessage testSets;

            if (rankQuesTionId != null)
            {
                testSets = await _httpClient.GetAsync($"{apiUrl}/getByRank/{rankQuesTionId}/{page}/{pageSize}");
            }
            else
            {
                testSets = await _httpClient.GetAsync($"{apiUrl}/forpagging/{page}/{pageSize}");
            }

            if (testSets.IsSuccessStatusCode)
            {
                var testSetData = await testSets.Content.ReadFromJsonAsync<PageResultDto<TestSetForUserDto>>();
                return View(testSetData);
            }
            return View(new PageResultDto<TestSetForUserDto> { Items = new List<TestSetForUserDto>(), Page = page, PageSize = pageSize, TotalItem = 0 });
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // Gọi API lấy đề thi
            var testSet = await _httpClient.GetFromJsonAsync<TestSetDetailsDto>($"{apiUrl}/getById/{id}");
            if (testSet == null)
            {
                return NotFound("Không tìm thấy bộ đề thi với ID này.");
            }

            // Gọi API lấy thông tin người dùng
            var response = await _httpClient.GetAsync($"https://localhost:7208/api/user/getById/{testSet.CreatedBy}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Không tìm thấy người dùng.");
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            // Phân tích JSON và lấy "data" ra
            using var doc = JsonDocument.Parse(jsonString);
            var root = doc.RootElement;

            if (!root.TryGetProperty("data", out var dataElement))
            {
                return NotFound("Không có thông tin người dùng.");
            }

            // Chuyển "data" thành UserOwnerDto
            var user = JsonSerializer.Deserialize<UserOwnerDto>(dataElement.GetRawText(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            ViewData["UserInfo"] = user;

            return View(testSet);
        }


        [HttpGet]
        public async Task<IActionResult> DoTestSet(int id, int timeLimit)
        {

            // Kiểm tra xem người dùng đã đăng nhập chưa bằng Session JWTToken
            var token = HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                // Nếu chưa có token, chuyển hướng về trang đăng nhập
                return RedirectToAction("Login", "User");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var name = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var avatar = jwtToken.Claims.FirstOrDefault(c => c.Type == "avatar")?.Value;

            // Bạn có thể lưu timeLimit vào ViewData hoặc TempData để truyền vào View
            ViewData["TimeLimit"] = timeLimit;
            ViewData["UserName"] = name;
            ViewData["UserAvatar"] = avatar;

            // Nếu đã đăng nhập thì gọi API và hiển thị bộ đề thi
            var testSet = await _httpClient.GetFromJsonAsync<DoTestSetDto>($"{apiUrl}/getDoById/{id}");
            if (testSet == null)
            {
                return NotFound("Không tìm thấy bộ đề thi với ID này.");
            }

            return View(testSet);
        }



        [HttpPost]
        public async Task<IActionResult> SubmitTest(SubmitTestDto model)
        {
            if (!ModelState.IsValid || model.Answers == null || !model.Answers.Any())
            {
                return BadRequest(new { message = "Bạn chưa trả lời câu hỏi nào." });
            }

            try
            {
                // Lấy token từ session hoặc nơi lưu trữ token
                var token = HttpContext.Session.GetString("JWTToken"); // hoặc lấy từ Cookie, TempData...

                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "Token xác thực không tồn tại hoặc đã hết hạn." });
                }

                // Thêm Authorization header vào HttpClient
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);




                var response = await _httpClient.PostAsJsonAsync(apiUrl + "/scoring", model);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(500, new { message = "Có lỗi xảy ra khi chấm điểm bài thi." });
                }

                var result = await response.Content.ReadFromJsonAsync<DetailedScoringResultDto>();

                // Lưu kết quả chi tiết vào TempData
                TempData["DetailedResult"] = JsonSerializer.Serialize(result);
                TempData["Score"] = result.CorrectCount;
                TempData["Total"] = result.TotalQuestions;
                TempData["Message"] = $"Bạn đã hoàn thành bài thi. Số câu đúng: {result.CorrectCount}/{result.TotalQuestions}";
                return RedirectToAction("Result");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống: " + ex.Message });
            }
        }


        public IActionResult Result()
        {
            if (TempData["DetailedResult"] == null || TempData["Score"] == null || TempData["Total"] == null || TempData["Message"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var detailedResult = JsonSerializer.Deserialize<DetailedScoringResultDto>(TempData["DetailedResult"].ToString());
            ViewBag.Score = TempData["Score"];
            ViewBag.Total = TempData["Total"];
            ViewBag.Message = TempData["Message"];
            ViewBag.DetailedResult = detailedResult;

            return View();
        }




        [HttpGet]
        public async Task<IActionResult> TestSetDetailsForAdmin(int testSetId, int page = 1, int pageSize = 1000)
        {
            try 
            {
                // Lấy thông tin testset
                var testSetApiUrl = $"https://localhost:7208/api/testset/getByIdForAdmin/{testSetId}";
                Console.WriteLine($"[TestSetDetailsForAdmin] Calling TestSet API: {testSetApiUrl}");
                
                var testSetResponse = await _httpClient.GetAsync(testSetApiUrl);
                Console.WriteLine($"[TestSetDetailsForAdmin] TestSet API Response Status: {testSetResponse.StatusCode}");
                
                if (!testSetResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[TestSetDetailsForAdmin] Failed to get test set. Status: {testSetResponse.StatusCode}");
                    return NotFound("Không tìm thấy đề thi.");
                }

                var testSet = await testSetResponse.Content.ReadFromJsonAsync<TestSetDetailsForAdmin>();
                if (testSet == null)
                {
                    Console.WriteLine("[TestSetDetailsForAdmin] Test set data is null");
                    return NotFound("Không thể đọc thông tin đề thi.");
                }

                ViewBag.RankQuestionId = testSet.RankQuestionId;
                HttpResponseMessage questions;

                if ((testSet.RankQuestionId >= 1 && testSet.RankQuestionId <= 12) || testSet.RankQuestionId == 21)
                {
                    questions = await _httpClient.GetAsync($"https://localhost:7208/api/readingquestion/forAddTest/{testSet.RankQuestionId}/{page}/{pageSize}");
                    if (questions.IsSuccessStatusCode)
                    {
                        var readingQuestions = await questions.Content.ReadFromJsonAsync<PageResultDto<ReadingQsDto>>();
                        ViewData["Question"] = readingQuestions;
                    }
                }
                else if(testSet.RankQuestionId >= 13 && testSet.RankQuestionId <= 20)
                {
                    questions = await _httpClient.GetAsync($"https://localhost:7208/api/listeningquestion/forAddTest/{testSet.RankQuestionId}/{page}/{pageSize}");
                    if (questions.IsSuccessStatusCode)
                    {
                        var listeningQuestions = await questions.Content.ReadFromJsonAsync<PageResultDto<ListeningQsDto>>();
                        ViewData["Question"] = listeningQuestions;
                    }
                }

                return View(testSet);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TestSetDetailsForAdmin] Unexpected error: {ex.Message}");
                Console.WriteLine($"[TestSetDetailsForAdmin] Stack trace: {ex.StackTrace}");
                return StatusCode(500, "Có lỗi xảy ra khi tải trang chi tiết đề thi.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestSetQuestions([FromBody] UpdateTestSetQuestionsRequest request)
        {
            try
            {
                if (request == null || request.QuestionIds == null)
                {
                    return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
                }

                // Gọi API để cập nhật câu hỏi cho testset
                var apiUrl = "https://localhost:7208/api/testset/updateQuestions";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, new
                {
                    testSetId = request.TestSetId,
                    questionIds = request.QuestionIds,
                    rankQuestionId = request.RankQuestionId // Thêm rankQuestionId để xác định loại câu hỏi
                });

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[UpdateTestSetQuestions] API Error: {errorContent}");
                    return Json(new { success = false, message = "Không thể cập nhật câu hỏi cho đề thi." });
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
                return Json(new { success = true, message = result?.Message ?? "Cập nhật thành công." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UpdateTestSetQuestions] Error: {ex.Message}");
                Console.WriteLine($"[UpdateTestSetQuestions] Stack trace: {ex.StackTrace}");
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật câu hỏi." });
            }
        }



        public class ApiResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }








        [HttpGet]
        public async Task<IActionResult> CreateTestSetByAdmin(int? rankQuestionId)
        {
            try
            {
                // Lấy dữ liệu từ API (bất đồng bộ)
                var rankQuestions = await _httpClient.GetFromJsonAsync<IEnumerable<RankQuestionDto>>("https://localhost:7208/api/rankquestion/getall");
                ViewData["RankQuestions"] = rankQuestions;

                // Nếu là AJAX request (có header X-Requested-With: XMLHttpRequest)
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    if (rankQuestionId == null)
                    {
                        return Json(new { success = true, questions = new List<object>() });
                    }

                    HttpResponseMessage questionsResponse; // Đổi tên biến để rõ ràng hơn
                    if ((rankQuestionId >= 1 && rankQuestionId <= 12) || rankQuestionId == 21)
                    {
                        questionsResponse = await _httpClient.GetAsync($"https://localhost:7208/api/readingquestion/forAddTest2/{rankQuestionId}");
                        if (questionsResponse.IsSuccessStatusCode)
                        {
                            var readingQuestions = await questionsResponse.Content.ReadFromJsonAsync<List<ReadingQsDto>>();
                            return Json(new { success = true, questions = readingQuestions });
                        }
                        else
                        {
                             // Trả về lỗi nếu API không thành công
                             // Có thể đọc nội dung lỗi từ API response nếu cần chi tiết hơn
                             // return StatusCode((int)questionsResponse.StatusCode, await questionsResponse.Content.ReadAsStringAsync()); // Tùy chọn: trả về mã lỗi và nội dung lỗi từ API
                             string errorMessage = $"Lỗi khi lấy câu hỏi Reading từ API. Status: {questionsResponse.StatusCode}";
                             Console.WriteLine($"[CreateTestSetByAdmin] API error: {errorMessage}");
                             return Json(new { success = false, message = "Không thể tải câu hỏi Reading từ máy chủ." }); // Báo lỗi rõ ràng cho view
                        }
                    }
                    else if (rankQuestionId >= 13 && rankQuestionId <= 20)
                    {
                        questionsResponse = await _httpClient.GetAsync($"https://localhost:7208/api/listeningquestion/forAddTest2/{rankQuestionId}");
                        if (questionsResponse.IsSuccessStatusCode)
                        {
                            var listeningQuestions = await questionsResponse.Content.ReadFromJsonAsync<List<ListeningQsDto>>();
                            return Json(new { success = true, questions = listeningQuestions });
                        }
                        else
                        {
                            // Trả về lỗi nếu API không thành công
                            string errorMessage = $"Lỗi khi lấy câu hỏi Listening từ API. Status: {questionsResponse.StatusCode}";
                            Console.WriteLine($"[CreateTestSetByAdmin] API error: {errorMessage}");
                            return Json(new { success = false, message = "Không thể tải câu hỏi Listening từ máy chủ." }); // Báo lỗi rõ ràng cho view
                        }
                    }
                    
                    // Trường hợp rankQuestionId không hợp lệ
                    return Json(new { success = false, message = "Cấp độ câu hỏi không hợp lệ." });
                }

                // Nếu là request thông thường (render view)
                if (rankQuestionId != null)
                {
                    HttpResponseMessage questions;
                    if ((rankQuestionId >= 1 && rankQuestionId <= 12) || rankQuestionId == 21)
                    {
                        questions = await _httpClient.GetAsync($"https://localhost:7208/api/readingquestion/forAddTest2/{rankQuestionId}");
                        if (questions.IsSuccessStatusCode)
                        {
                            var readingQuestions = await questions.Content.ReadFromJsonAsync<List<ReadingQsDto>>();
                            ViewData["Question"] = readingQuestions;
                        }
                    }
                    else if (rankQuestionId >= 13 && rankQuestionId <= 20)
                    {
                        questions = await _httpClient.GetAsync($"https://localhost:7208/api/listeningquestion/forAddTest2/{rankQuestionId}");
                        if (questions.IsSuccessStatusCode)
                        {
                            var listeningQuestions = await questions.Content.ReadFromJsonAsync<List<ListeningQsDto>>();
                            ViewData["Question"] = listeningQuestions;
                        }
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CreateTestSetByAdmin] Unexpected error: {ex.Message}");
                Console.WriteLine($"[CreateTestSetByAdmin] Stack trace: {ex.StackTrace}");
                return StatusCode(500, "Có lỗi xảy ra khi tải trang tạo đề thi.");
            }
        }

    }
}
