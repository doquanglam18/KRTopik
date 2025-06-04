using DATN.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace DATN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeminiApiController : ControllerBase
    {
        private readonly GeminiService _geminiService;
        private const string SESSION_COOKIE_NAME = "gemini_session";
        private const string CHAT_VISIBILITY_COOKIE = "chat_visibility";

        public GeminiApiController(GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        private string GetCurrentUserId()
        {
            // Always use session ID as user ID for guests
            var sessionId = GetOrCreateSessionId();
            return $"guest_{sessionId}";
        }

        private string GetOrCreateSessionId()
        {
            var sessionId = Request.Cookies[SESSION_COOKIE_NAME];
            
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                SetSessionCookie(sessionId);
            }

            return sessionId;
        }

        private void SetSessionCookie(string sessionId)
        {
            Response.Cookies.Append(SESSION_COOKIE_NAME, sessionId, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(1)
            });
        }

        private void ClearSessionCookies()
        {
            Response.Cookies.Delete(SESSION_COOKIE_NAME, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
            });
            Response.Cookies.Delete(CHAT_VISIBILITY_COOKIE, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
            });
        }

        // POST: api/GeminiApi/ask
        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] AskRequest request)
        {
            try
            {
                Console.WriteLine("Received request in Ask endpoint");
                Console.WriteLine($"Request body: {JsonSerializer.Serialize(request)}");
                Console.WriteLine($"Content-Type: {Request.ContentType}");
                Console.WriteLine($"Headers: {string.Join(", ", Request.Headers.Select(h => $"{h.Key}: {h.Value}"))}");

                if (request == null)
                {
                    Console.WriteLine("Invalid request: Request body is null");
                    return BadRequest(new { success = false, message = "Request body is required" });
                }

                if (string.IsNullOrWhiteSpace(request.Prompt))
                {
                    Console.WriteLine("Invalid request: Empty prompt");
                    return BadRequest(new { success = false, message = "Prompt cannot be empty" });
                }

                // Always create a new session ID for each request
                var sessionId = Guid.NewGuid().ToString();
                SetSessionCookie(sessionId);
                var userId = GetCurrentUserId();

                Console.WriteLine($"Processing request for user {userId}");
                Console.WriteLine($"New Session ID: {sessionId}");
                Console.WriteLine($"Prompt: {request.Prompt}");

                try
                {
                    Console.WriteLine("Calling GeminiService.GetResponseFromGemini");
                    var response = await _geminiService.GetResponseFromGemini(request.Prompt, sessionId, userId);
                    Console.WriteLine("Successfully got response from Gemini");
                    
                    return Ok(new { 
                        success = true, 
                        message = response,
                        sessionId = sessionId 
                    });
                }
                catch (GeminiServiceException ex)
                {
                    Console.WriteLine($"GeminiService error: {ex.Message}");
                    Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                    return StatusCode(500, new { 
                        success = false, 
                        message = "Error communicating with Gemini API",
                        details = ex.Message
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Ask endpoint: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner exception stack trace: {ex.InnerException.StackTrace}");
                }
                return StatusCode(500, new { 
                    success = false, 
                    message = "An error occurred while processing your request",
                    details = ex.Message
                });
            }
        }

        // GET: api/GeminiApi/models
        [HttpGet("models")]
        public async Task<IActionResult> ListModels()
        {
            try
            {
                Console.WriteLine("Listing available models");
                var models = await _geminiService.ListAvailableModels();
                Console.WriteLine($"Found {models.Count} models: {string.Join(", ", models)}");
                return Ok(new { success = true, models = models });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing models: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Failed to list models", details = ex.Message });
            }
        }

        // POST: api/GeminiApi/clear-history
        [HttpPost("clear-history")]
        public IActionResult ClearHistory([FromBody] ClearHistoryRequest request)
        {
            try
            {
                // Clear all chat histories for the current session
                var userId = GetCurrentUserId();
                _geminiService.ClearUserChatHistories(userId);
                
                // Create new session
                var newSessionId = Guid.NewGuid().ToString();
                SetSessionCookie(newSessionId);

                return Ok(new { 
                    success = true, 
                    message = "Chat history cleared successfully",
                    sessionId = newSessionId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing chat history: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Failed to clear chat history", details = ex.Message });
            }
        }

        // POST: api/GeminiApi/logout
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] LogoutRequest request)
        {
            try
            {
                // Clear all chat histories for the current user
                var userId = GetCurrentUserId();
                _geminiService.ClearUserChatHistories(userId);

                // Clear all session cookies
                ClearSessionCookies();

                return Ok(new { success = true, message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during logout: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Failed to logout", details = ex.Message });
            }
        }

        // POST: api/GeminiApi/toggle-visibility
        [HttpPost("toggle-visibility")]
        public IActionResult ToggleVisibility([FromBody] ToggleVisibilityRequest request)
        {
            try
            {
                if (request == null || !request.IsVisible.HasValue)
                {
                    return BadRequest(new { success = false, message = "Visibility state is required" });
                }

                // Store visibility state in cookie
                Response.Cookies.Append(CHAT_VISIBILITY_COOKIE, request.IsVisible.Value.ToString(), new Microsoft.AspNetCore.Http.CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict,
                    Expires = DateTime.Now.AddDays(1)
                });

                return Ok(new { success = true, message = "Visibility state updated" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error toggling visibility: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Failed to update visibility", details = ex.Message });
            }
        }

        // GET: api/GeminiApi/visibility
        [HttpGet("visibility")]
        public IActionResult GetVisibility()
        {
            try
            {
                var visibility = Request.Cookies[CHAT_VISIBILITY_COOKIE];
                var isVisible = string.IsNullOrEmpty(visibility) ? true : bool.Parse(visibility);
                return Ok(new { success = true, isVisible = isVisible });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting visibility: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Failed to get visibility state", details = ex.Message });
            }
        }
    }

    // DTO để nhận prompt từ phía client
    public class GeminiPromptDto
    {
        public string Prompt { get; set; }
    }

    public class AskRequest
    {
        public string Prompt { get; set; }
        public string? SessionId { get; set; }
    }

    public class ClearHistoryRequest
    {
        public string SessionId { get; set; }
    }

    public class LogoutRequest
    {
        public string SessionId { get; set; }
    }

    public class ToggleVisibilityRequest
    {
        public bool? IsVisible { get; set; }
    }
}
