using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.RoleDtos;
using DATN.Application.Dtos.SystemLoggingDtos;
using DATN.Application.Dtos.SystemLoggingDtos.Chart;
using DATN.Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DATN.WebApp.Controllers
{
    public class SystemAdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://localhost:7208/api/user";
        private const string apiBaseUrl = "https://localhost:7208/api/systemlogging";

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



        [HttpGet]
        public async Task<IActionResult> AccessStats(DateTime? fromDate, DateTime? toDate)
        {
            // Lấy JWT token từ session
            var token = HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }

            // Thêm token vào header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Xử lý ngày để lấy từ đầu ngày đến cuối ngày
            var fromDateTime = fromDate.HasValue ? fromDate.Value.Date : (DateTime?)null;
            var toDateTime = toDate.HasValue ? toDate.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

            // Gọi API để lấy dữ liệu thống kê
            var queryParams = $"?from={fromDateTime?.ToString("yyyy-MM-ddTHH:mm:ss")}&to={toDateTime?.ToString("yyyy-MM-ddTHH:mm:ss")}";
            var statsResponse = await _httpClient.GetFromJsonAsync<IEnumerable<AccessStatsDto>>($"{apiBaseUrl}/stats{queryParams}");

            // Gọi API lấy dữ liệu vẽ biểu đồ
            var chartResponse = await _httpClient.GetFromJsonAsync<IEnumerable<ChartDataDto>>($"{apiBaseUrl}/chart{queryParams}");

            // Tạo view model
            var viewModel = new AccessStatsViewModel
            {
                Stats = statsResponse?.ToList() ?? new List<AccessStatsDto>(),
                ChartData = chartResponse?.ToList() ?? new List<ChartDataDto>(),
                FromDate = fromDate,
                ToDate = toDate
            };

            return View(viewModel);
        }

 
    }

}

