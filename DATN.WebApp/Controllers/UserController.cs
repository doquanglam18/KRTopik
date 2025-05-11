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
using System.Security.Claims;
using System.Text;
using System.Text.Json;
namespace DATN.WebApp.Controllers
{
    public class UserController : Controller
    {

        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://localhost:7208/api/user";

        public UserController(HttpClient httpClient )
        {
            _httpClient = httpClient;
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
        public IActionResult LogOut()
        {
            // Xóa session hoặc xác thực
            HttpContext.Session.Clear();

            return Json(new
            {
                success = true,
                message = "Đăng xuất thành công",
                redirectUrl = Url.Action("Index", "Home")
            });
        }



    }
}
