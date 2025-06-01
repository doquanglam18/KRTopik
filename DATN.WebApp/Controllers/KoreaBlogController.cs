using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Dtos.KoreaBlogDtos;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.TestSetDtos;
using DATN.Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DATN.WebApp.Controllers
{
    public class KoreaBlogController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://localhost:7208/api/koreablog";

        public KoreaBlogController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> ListKoreaBlogForUser(string? searchBlog, int page = 1, int pageSize = 5)
        {

            HttpResponseMessage koreaBlogs;

            if (searchBlog != null)
            {
                koreaBlogs = await _httpClient.GetAsync($"{apiUrl}/search/{searchBlog}/{page}/{pageSize}");
            }
            else
            {
                koreaBlogs = await _httpClient.GetAsync($"{apiUrl}/forpagging/{page}/{pageSize}");
            }

            if (koreaBlogs.IsSuccessStatusCode)
            {
                var koreaBlogsData = await koreaBlogs.Content.ReadFromJsonAsync<PageResultDto<KoreaBlogForList>>();
                return View(koreaBlogsData);
            }
            return View(new PageResultDto<KoreaBlogForList> { Items = new List<KoreaBlogForList>(), Page = page, PageSize = pageSize, TotalItem = 0 });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // Gọi API lấy đề thi
            var koreaBlog = await _httpClient.GetFromJsonAsync<KoreaBlogDetailsDto>($"{apiUrl}/getById/{id}");
            if (koreaBlog == null)
            {
                return NotFound("Không tìm thấy bài viết này.");
            }

            // Gọi API lấy thông tin người dùng
            var response = await _httpClient.GetAsync($"https://localhost:7208/api/user/getById/{koreaBlog.CreateadBy}");
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

            return View(koreaBlog);
        }


        [HttpGet]
        public IActionResult CreateKoreaBlog()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var result = await _httpClient.PutAsync($"{apiUrl}/updateStatus/{id}", null);
            if(result.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = $"Thay đổi trạng thái bài viết thành công !" });
            }
            else
            {
                return Json(new { success = false, message = $"Thay đổi trạng thái bài viết thất bại !" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateKoreaBlog(KoreaBlogCreateDto koreaBlogCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(koreaBlogCreateDto);
            }

            try
            {
                var token = HttpContext.Session.GetString("JWTToken");
                if (string.IsNullOrEmpty(token))
                {
                    return Unauthorized(new { message = "Token xác thực không tồn tại hoặc đã hết hạn." });
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(koreaBlogCreateDto.Title ?? ""), "Title");
                formData.Add(new StringContent(koreaBlogCreateDto.Content ?? ""), "Content");
                formData.Add(new StringContent(koreaBlogCreateDto.TitleVietSub ?? ""), "TitleVietSub");
                formData.Add(new StringContent(koreaBlogCreateDto.VietSubContent ?? ""), "VietSubContent");

                // Nếu có file ảnh:
                if (koreaBlogCreateDto.Image != null && koreaBlogCreateDto.Image.Length > 0)
                {
                    var stream = koreaBlogCreateDto.Image.OpenReadStream();
                    var fileContent = new StreamContent(stream);
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(koreaBlogCreateDto.Image.ContentType);
                    formData.Add(fileContent, "Image", koreaBlogCreateDto.Image.FileName);
                }

                var response = await _httpClient.PostAsync($"{apiUrl}/create", formData);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = $"Đăng bài viết thành công, hãy chờ Admin duyệt bài viết của bạn !" });
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"Đăng bài viết thất bại: {errorContent}" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi hệ thống: {ex.Message}" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"{apiUrl}/delete/{id}");
            if (result.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = $"Xóa bài viết thành công !" });
            }
            else
            {
                return Json(new { success = false, message = $"Xóa bài viết thất bại !" });
            }
        }



    }
}
