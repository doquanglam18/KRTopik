using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Dtos.UserDtos;
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
        public IActionResult ManageTestSet()
        {
            return View();
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
    }
}
