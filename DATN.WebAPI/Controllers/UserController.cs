using AutoMapper;
using DATN.Application.Dtos.UserDtos;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using DATN.Application.Services.Implements;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DATN.Infrastructure.Context;
using DATN.Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DATN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly ISystemLoggingService _loggingService;

        public UserController(ISystemLoggingService systemLoggingService ,IUserService userService, IMapper mapper, IEmailService emailService, ITokenService tokenService, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _emailService = emailService;
            _tokenService = tokenService;
            _configuration = configuration;
            _loggingService = systemLoggingService;

        }

        [Authorize("SystemAdminOnly")]
        [HttpGet("getalluser")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllUserAsync();
            var usersDto = _mapper.Map<IEnumerable<UserDetailDto>>(users);
            return Ok(usersDto);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            try
            {
                ResultV<User> userResultV = await _userService.CheckLogin(request.Email, request.PasswordHash);

                if (!userResultV.IsSuccess)
                {
                    // Ghi log đăng nhập thất bại
                    await _loggingService.LogAction(
                        userId: null,
                        ipAddress: HttpContext.Connection.RemoteIpAddress?.ToString(),
                        actionName: "Login - Failed",
                        details: $"Email: {request.Email} - Lý do: {userResultV.Message}"
                    );

                    return BadRequest(ResultV<UserTokenDTO>.Failure(userResultV.Message));
                }

                User user = userResultV.Data;
                var userLogin = _mapper.Map<UserTokenDTO>(user);

                var jwtSettings = _configuration.GetSection("JwtSettings");
                var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userLogin.Id.ToString()),
                    new Claim(ClaimTypes.Name, userLogin.FullName),
                    new Claim(ClaimTypes.Email, userLogin.Email),
                    new Claim(ClaimTypes.Role, userLogin.RoleName),
                    new Claim("avatar", user.AvatarImageUrl)
                };

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                userLogin.Token = new JwtSecurityTokenHandler().WriteToken(token);

                // Ghi log đăng nhập thành công
                await _loggingService.LogAction(
                    userId: userLogin.Id,
                    ipAddress: HttpContext.Connection.RemoteIpAddress?.ToString(),
                    actionName: "Login - Success",
                    details: $"User {userLogin.FullName} ({userLogin.Email}) đã đăng nhập thành công."
                );

                return Ok(ResultV<UserTokenDTO>.Success(userLogin, "Đăng nhập thành công !"));
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hệ thống khi đăng nhập
                await _loggingService.LogAction(
                    userId: null,
                    ipAddress: HttpContext.Connection.RemoteIpAddress?.ToString(),
                    actionName: "Login - Error",
                    details: $"Lỗi hệ thống: {ex.Message}"
                );

                return StatusCode(500, new { message = "Đã xảy ra lỗi khi đăng nhập.", detail = ex.Message });
            }
        }




        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash),
                DateOfBirth = dto.DateOfBirth,
                RoleId = 3
            };

            // Tạo token xác nhận
            string token = _tokenService.GenerateVerificationToken(user);
            user.VerificationToken = token;

            // Lưu user
            var result = await _userService.AddUserAsync(user);

            if (!result.IsSuccess)
            {
                return BadRequest(Result.Failure("Email đã tồn tại"));
            }

            // Gửi email xác nhận
            var verifyUrl = $"{Request.Scheme}://{Request.Host}/api/user/verify?token={Uri.EscapeDataString(token)}";
            string emailBody = $@"
            <p>Chào {user.FullName},</p>
            <p>Vui lòng <a href='{verifyUrl}'>bấm vào đây</a> để xác nhận tài khoản của bạn.</p>";

            await _emailService.SendEmailAsync(user.Email, "Xác nhận đăng ký tài khoản", emailBody);

            return Ok(Result.Success("Đăng ký thành công! Vui lòng kiểm tra email để xác nhận tài khoản."));
        }



        [HttpGet("verify")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            token = Uri.UnescapeDataString(token);

            var users = await _userService.GetAllUserAsync();
            var user = users.FirstOrDefault(u => u.VerificationToken?.Trim() == token?.Trim());

            string message;
            string icon;
            bool success = false;

            if (user == null)
            {
                message = "Token xác nhận không hợp lệ. Vui lòng liên hệ với Người quản trị hệ thống !";
                icon = "error";
            }
            else if (user.IsActive)
            {
                message = "Tài khoản đã được xác nhận trước đó.";
                icon = "info";
                success = true;
            }
            else
            {
                user.IsActive = true;
                await _userService.UpdateUserAsync(user);

                message = "Tài khoản của bạn đã được xác nhận thành công!";
                icon = "success";
                success = true;
            }

            var html = $@"
            <!DOCTYPE html>
            <html lang='vi'>
            <head>
                <meta charset='UTF-8'>
                <title>Xác nhận tài khoản</title>
                <script src='https://cdn.jsdelivr.net/npm/sweetalert2@11'></script>
            </head>
            <body>
                <script>
                    Swal.fire({{
                        icon: '{icon}',
                        title: 'Thông báo',
                        text: '{message}',
                        confirmButtonText: 'OK'
                    }}).then((result) => {{
                        if (result.isConfirmed) {{
                            window.location.href = 'https://localhost:7161/Home/Index'; // Chuyển về trang Home/Index
                        }}
                    }});
                </script>
            </body>
            </html>";   

            return Content(html, "text/html");
        }






        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDetailDto userDetailDto)
        {
            if (userDetailDto == null)
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ!" });

            var user = _mapper.Map<User>(userDetailDto);
            var result = await _userService.UpdateUserAsync(user);

            if (result.IsSuccess)
            {
                return Ok(new { success = true, message = result.Message });
            }
            else
            {
                return BadRequest(new { success = false, message = result.Message });
            }
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { success = false, message = "Người dùng không tồn tại!" });
            }

            var userDto = _mapper.Map<UserOwnerDto>(user);
            return Ok(new { success = true, data = userDto });
        }




    }
}
