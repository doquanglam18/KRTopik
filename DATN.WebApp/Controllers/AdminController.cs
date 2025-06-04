using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Dtos.KoreaBlogDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Dtos.StatisticsDtos;
using DATN.Application.Dtos.TestSetDtos;
using DATN.Application.Dtos.TestSetDtos.ForAdmin;
using DATN.Application.Dtos.UserDtos;
using DATN.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace DATN.WebApp.Controllers
{
    public class AdminController : Controller
    {

        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://localhost:7208/api";

        public AdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> ManageReadingQuestion()
        {
            // Lấy JWT token từ session
            var token = HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                // Nếu chưa có token, chuyển về trang đăng nhập
                return RedirectToAction("Login", "User");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var readingQuestions = await _httpClient.GetFromJsonAsync<IEnumerable<ReadingQuestionDto>>($"{apiUrl}/readingquestion/getall");

            return View(readingQuestions);
        }

        public async Task<IActionResult> ManageListeningQuestion(int page = 1, int pageSize = 5)
        {
            var token = HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"{apiUrl}/listeningquestion/forpagging/{page}/{pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<PageResultDto<ListeningQuestionDto>>();
                return View(data);
            }

            return View(new PageResultDto<ListeningQuestionDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItem = 0,
                Items = new List<ListeningQuestionDto>()
            });
        }

        public async Task<IActionResult> ManageTestSet(int? rankQuestionId, int page = 1, int pageSize = 5)
        {
            var rankQuestions = await _httpClient.GetFromJsonAsync<IEnumerable<RankQuestionDto>>("https://localhost:7208/api/rankquestion/getall");

            // Lưu vào ViewData
            ViewData["rankQuestionId"] = rankQuestionId;
            ViewData["RankQuestions"] = rankQuestions;
            HttpResponseMessage testSets;

            if (rankQuestionId != null)
            {
                testSets = await _httpClient.GetAsync($"{apiUrl}/testset/getByRankadmin/{rankQuestionId}/{page}/{pageSize}");
            }
            else
            {
                testSets = await _httpClient.GetAsync($"{apiUrl}/testset/forpaggingadmin/{page}/{pageSize}");
            }

            if (testSets.IsSuccessStatusCode)
            {
                var testSetData = await testSets.Content.ReadFromJsonAsync<PageResultDto<ListTestSetForAdmin>>();
                return View(testSetData);
            }
            return View(new PageResultDto<ListTestSetForAdmin> { Items = new List<ListTestSetForAdmin>(), Page = page, PageSize = pageSize, TotalItem = 0 });
        }


        public async Task<IActionResult> ManageKoreaBlog(int page = 1, int pageSize = 5)
        {
            HttpResponseMessage koreaBlogs;

            koreaBlogs = await _httpClient.GetAsync($"{apiUrl}/koreablog/forpaggingadmin/{page}/{pageSize}");

            if (koreaBlogs.IsSuccessStatusCode)
            {
                var koreaBlogData = await koreaBlogs.Content.ReadFromJsonAsync<PageResultDto<KoreaBlogForList>>();
                return View(koreaBlogData);
            }
            return View(new PageResultDto<KoreaBlogForList> { Items = new List<KoreaBlogForList>(), Page = page, PageSize = pageSize, TotalItem = 0 });

        }

        [HttpGet]
        public async Task<IActionResult> TestSetStatistics()
        {
            // Kiểm tra token
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }

            try
            {
                // Thêm token vào header
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                // Lấy danh sách cấp độ
                var rankQuestions = await _httpClient.GetFromJsonAsync<IEnumerable<RankQuestionDto>>($"{apiUrl}/rankquestion/getall");
                if (rankQuestions == null)
                {
                    // Nếu không lấy được dữ liệu, trả về danh sách rỗng
                    ViewData["RankQuestions"] = new List<RankQuestionDto>();
                    return View();
                }

                // Lưu vào ViewData
                ViewData["RankQuestions"] = rankQuestions;
                return View();
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                ViewData["RankQuestions"] = new List<RankQuestionDto>();
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetTestSetStatistics([FromBody] TestSetStatisticsRequestDto request)
        {
            try
            {
                // Lấy JWT token từ session
                var token = HttpContext.Session.GetString("JWTToken");

                if (string.IsNullOrEmpty(token))
                {
                    // Nếu chưa có token, chuyển về trang đăng nhập
                    return RedirectToAction("Login", "User");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync($"{apiUrl}/TestSetStatistics/statistics", request);
                if (response.IsSuccessStatusCode)
                {
                    var statistics = await response.Content.ReadFromJsonAsync<List<TestSetStatisticsDto>>();
                    return Json(new { success = true, data = statistics });
                }
                return Json(new { success = false, message = "Failed to get statistics" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetTestSetStatisticsByRank(int rankId, [FromBody] TestSetStatisticsRequestDto request)
        {
            try
            {
                // Lấy JWT token từ session
                var token = HttpContext.Session.GetString("JWTToken");

                if (string.IsNullOrEmpty(token))
                {
                    // Nếu chưa có token, chuyển về trang đăng nhập
                    return RedirectToAction("Login", "User");
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsJsonAsync($"{apiUrl}/TestSetStatistics/statistics/rank/{rankId}", request);
                if (response.IsSuccessStatusCode)
                {
                    var statistics = await response.Content.ReadFromJsonAsync<List<TestSetStatisticsDto>>();
                    return Json(new { success = true, data = statistics });
                }
                return Json(new { success = false, message = "Failed to get statistics" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


    }
}
