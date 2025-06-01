using DATN.Application.Dtos.UserDtos;
using DATN.Application.Services;
using DATN.Domain.Entities;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
namespace DATN.WebApp.Controllers
{
    public class UserController : Controller
    {

        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://localhost:7208/api/user";
        private readonly ILogger<UserController> _logger;

        public UserController(HttpClient httpClient, ILogger<UserController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost("loginApp")]
        public async Task<IActionResult> LoginApp( LoginDto loginDto)
        {

                var response = await _httpClient.PostAsJsonAsync($"{apiUrl}/login", loginDto);

                var result = await response.Content.ReadFromJsonAsync<ResultV<UserTokenDTO>>();
                var user = result.Data;

                if (!response.IsSuccessStatusCode)
                {
                    
                    return Json(new
                    {
                        success = false,
                        message = result.Message,
                        redirectUrl = ""
                    }); ;
                }
                else
                {
                    HttpContext.Session.SetString("JWTToken", user.Token);
                    if(user.Email == "admin@gmail.com" && user.RoleName == "SystemAdmin")
                    {
                        return Json(new
                        {
                            success = true,
                            message = result?.Message ?? "Đăng nhập thành công với vai trò System Admin!",
                            redirectUrl = Url.Action("Index", "SystemAdmin")
                        });

                    }
                else
                    {
                        return Json(new
                        {
                            success = true,
                            message = result?.Message ?? "Đăng nhập thành công!",
                            redirectUrl = Url.Action("Index", "Home")
                        });
                    }

            }   
        }



        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
 
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Dữ liệu không hợp lệ",
                        redirectUrl = ""  
                    });
                }

                var response = await _httpClient.PostAsJsonAsync($"{apiUrl}/register", dto);
                var result = await response.Content.ReadFromJsonAsync<Result>();

                if (!response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        success = false,
                        message = result?.Message ?? "Đã xảy ra lỗi khi đăng ký.",
                        redirectUrl = ""
                    });
                }

                return Json(new
                {
                    success = true,
                    message = result?.Message ?? "Đăng ký thành công! Vui lòng kiểm tra email để xác nhận tài khoản.",
                    redirectUrl = Url.Action("Login", "User")
                });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            var logData = new Dictionary<string, string>
            {
                { "actionVName", "Logout" },
                { "details", "Người dùng đã đăng xuất khỏi hệ thống" }
            };

            // Lấy token từ session thay vì header
            var token = HttpContext.Session.GetString("JWTToken");
            _logger.LogInformation("Token from session: {Token}", token);

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _logger.LogWarning("Không tìm thấy token trong session");
            }

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7208/api/systemlogging/loggingaction", logData);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogWarning("Ghi log thất bại khi đăng xuất: {StatusCode}, Error: {Error}", response.StatusCode, errorContent);
            }
            else
            {
                _logger.LogInformation("Ghi log đăng xuất thành công");
            }

            HttpContext.Session.Clear();

            return Json(new
            {
                success = true,
                message = "Đăng xuất thành công",
                redirectUrl = Url.Action("Index", "Home")
            });
        }



        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{apiUrl}/forgotpassword", dto);
            var result = await response.Content.ReadFromJsonAsync<Result>();
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = result?.Message ?? "Không thể gửi email." });
            }
            return Json(new { success = true, message = "Vui lòng kiểm tra email để đặt lại mật khẩu." });
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync($"{apiUrl}/resetpassword", dto);
            var result = await response.Content.ReadFromJsonAsync<Result>();
            if (!response.IsSuccessStatusCode)
            {
                return Json(new { success = false, message = result?.Message ?? "Không thể đặt lại mật khẩu." });
            }
            return Json(new { success = true, message = "Đặt lại mật khẩu thành công!" });
        }


        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }

            try
            {

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"{apiUrl}/getProfileUser");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
                    
                    if (jsonResponse != null && 
                        jsonResponse.ContainsKey("success") && 
                        Convert.ToBoolean(jsonResponse["success"]) && 
                        jsonResponse.ContainsKey("data"))
                    {
                        var userData = JsonConvert.DeserializeObject<UserDetailForUserDto>(jsonResponse["data"].ToString());
                        return View(userData);
                    }
                }
                
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    HttpContext.Session.Remove("JWTToken");
                    return RedirectToAction("Login", "User");
                }

                TempData["Error"] = "Không thể tải thông tin cá nhân. Vui lòng thử lại sau.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tải thông tin cá nhân");
                TempData["Error"] = "Đã xảy ra lỗi khi tải thông tin cá nhân.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
