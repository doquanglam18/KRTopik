using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.TestSetDtos;
using DATN.Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

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
        public async Task<IActionResult> DoTestSet(int id)
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



    }
}
