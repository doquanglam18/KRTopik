using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.RoleDtos;
using DATN.Application.Dtos.SystemLoggingDtos;
using DATN.Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DATN.WebApp.Controllers
{
    public class SystemAdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://localhost:7208/api/user";

        public SystemAdminController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            // Lấy JWT token từ session
            var token = HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                // Nếu chưa có token, chuyển về trang đăng nhập
                return RedirectToAction("Login", "User");
            }

			// Lấy dữ liệu từ API (bất đồng bộ)
			var roles = await _httpClient.GetFromJsonAsync<IEnumerable<RoleDto>>("https://localhost:7208/api/role/getall");

			// Lưu vào ViewData
			ViewData["Roles"] = roles;

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var users = await _httpClient.GetFromJsonAsync<IEnumerable<UserDetailDto>>($"{apiUrl}/getalluser");

            return View(users);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateLockUser([FromBody] UserDetailDto userViewAllDTO)
        {

            if (userViewAllDTO == null)
            {
                return BadRequest("Dữ liệu không hợp lệ!");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWTToken"));
            if (userViewAllDTO.IsActive == true)
            {
                userViewAllDTO.IsActive = false;
            }
            else
            {
                userViewAllDTO.IsActive = true;
            }
            var response = await _httpClient.PutAsJsonAsync(apiUrl, userViewAllDTO);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Thay đổi trạng thái người dùng thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Thay đổi trạng thái người dùng thất bại!" });
            }
        }


		[HttpPost]
		public async Task<IActionResult> ChangeUserRole([FromBody] UserDetailDto userViewAllDTO)
		{

			if (userViewAllDTO == null)
			{
				return BadRequest("Dữ liệu không hợp lệ!");
			}

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWTToken"));
			var response = await _httpClient.PutAsJsonAsync(apiUrl, userViewAllDTO);
			if (response.IsSuccessStatusCode)
			{
				return Json(new { success = true, message = "Thay đổi vai trò người dùng thành công!" });
			}
			else
			{
				return Json(new { success = false, message = "Thay đổi vai trò người dùng thất bại!" });
			}
		}


        [HttpGet]
        public async Task<IActionResult> ManageSystemLogging()
        {
            // Lấy JWT token từ session
            var token = HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                // Nếu chưa có token, chuyển về trang đăng nhập
                return RedirectToAction("Login", "User");
            }

            // Lấy dữ liệu từ API (bất đồng bộ)
            var systemLoggings = await _httpClient.GetFromJsonAsync<IEnumerable<SystemLoggingDto>>("https://localhost:7208/api/systemlogging/getall");


            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return View(systemLoggings);
        }



    }
}
