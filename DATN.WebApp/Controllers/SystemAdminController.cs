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
        // Trang thống kê chính
        public async Task<IActionResult> AccessStats(DateTime? fromDate, DateTime? toDate, string? actionName, string? ip, Guid? userId)
        {
            // Lấy JWT token từ session
            var token = HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                // Nếu chưa có token, chuyển về trang đăng nhập
                return RedirectToAction("Login", "User");
            }

            // Thêm token vào header
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Gọi API để lấy dữ liệu thống kê
            var queryParams = $"?fromDate={fromDate?.ToString("yyyy-MM-ddTHH:mm:ss")}&toDate={toDate?.ToString("yyyy-MM-ddTHH:mm:ss")}&actionName={actionName}&ip={ip}&userId={userId}";
            var statsResponse = await _httpClient.GetFromJsonAsync<IEnumerable<AccessStatsDto>>($"{apiBaseUrl}/stats{queryParams}");

            // Gọi API lấy dữ liệu vẽ biểu đồ
            var chartResponse = await _httpClient.GetFromJsonAsync<IEnumerable<ChartDataDto>>($"{apiBaseUrl}/chart{queryParams}");

            // Tạo view model
            var viewModel = new AccessStatsViewModel
            {
                Stats = statsResponse?.ToList() ?? new List<AccessStatsDto>(),
                ChartData = chartResponse?.ToList() ?? new List<ChartDataDto>(),
                FromDate = fromDate,
                ToDate = toDate,
                ActionName = actionName,
                IP = ip,
                UserId = userId
            };

            return View(viewModel);
        }

        // Xuất Excel
        public async Task<IActionResult> ExportExcel(DateTime? fromDate, DateTime? toDate, string? actionName, string? ip, Guid? userId)
        {
            var queryParams = $"?fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}&actionName={actionName}&ip={ip}&userId={userId}";
            var response = await _httpClient.GetAsync($"{apiBaseUrl}/export/excel{queryParams}");

            if (!response.IsSuccessStatusCode)
                return BadRequest("Không thể xuất Excel");

            var content = await response.Content.ReadAsByteArrayAsync();
            var fileName = $"AccessStats_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        // Xuất PDF
        public async Task<IActionResult> ExportPdf(DateTime? fromDate, DateTime? toDate, string? actionName, string? ip, Guid? userId)
        {
            var queryParams = $"?fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}&actionName={actionName}&ip={ip}&userId={userId}";
            var response = await _httpClient.GetAsync($"{apiBaseUrl}/export/pdf{queryParams}");

            if (!response.IsSuccessStatusCode)
                return BadRequest("Không thể xuất PDF");

            var content = await response.Content.ReadAsByteArrayAsync();
            var fileName = $"AccessStats_{DateTime.Now:yyyyMMddHHmmss}.pdf";

            return File(content, "application/pdf", fileName);
        }
    }

}

