using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Dtos.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace DATN.WebApp.Controllers
{
    public class ReadingQuestionController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://localhost:7208/api/readingquestion";

        public ReadingQuestionController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult UpdateReadingQuestion(ReadingQuestionDto readingQuestionDto)
        {
            return View(readingQuestionDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReadingQuestion(int id, ReadingQuestionDto updateDto, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy chỉ số đáp án đúng từ form (radio button)
                    var correctAnswerIndexString = Request.Form["CorrectAnswerIndex"];
                    if (int.TryParse(correctAnswerIndexString, out int correctAnswerIndex))
                    {
                        for (int i = 0; i < updateDto.ReadingAnswers.Count; i++)
                        {
                            updateDto.ReadingAnswers[i].IsCorrect = (i == correctAnswerIndex);
                        }
                    }

                    // Tạo MultipartFormDataContent để gửi yêu cầu đến API
                    var content = new MultipartFormDataContent();

                    // Gửi ảnh nếu có
                    if (image != null)
                    {
                        var fileContent = new StreamContent(image.OpenReadStream());
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(image.ContentType);
                        content.Add(fileContent, "image", image.FileName);
                    }

                    // Gửi các thuộc tính chính
                    content.Add(new StringContent(updateDto.Id.ToString()), "Id");

                    content.Add(new StringContent(updateDto.Question), "Question");
                    content.Add(new StringContent(updateDto.TestSetId.ToString()), "TestSetId");
                    content.Add(new StringContent(updateDto.RankQuestionId.ToString()), "RankQuestionId");
                    content.Add(new StringContent(updateDto.ReadingImageURL ?? string.Empty), "ReadingImageURL");
                    content.Add(new StringContent(updateDto.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ss")), "CreatedDate");
                    content.Add(new StringContent(updateDto.UpdatedDate.ToString("yyyy-MM-ddTHH:mm:ss")), "UpdatedDate");

                    content.Add(new StringContent(updateDto.IsPublic.ToString()), "IsPublic");

                    // Gửi danh sách ReadingAnswers
                    for (int i = 0; i < updateDto.ReadingAnswers.Count; i++)
                    {
                        var answer = updateDto.ReadingAnswers[i];
                        content.Add(new StringContent(answer.Id.ToString()), $"ReadingAnswers[{i}].Id");
                        content.Add(new StringContent(answer.Content), $"ReadingAnswers[{i}].Content");
                        content.Add(new StringContent(answer.IsCorrect.ToString().ToLower()), $"ReadingAnswers[{i}].IsCorrect");
                    }

                    // Gọi API cập nhật câu hỏi
                    var response = await _httpClient.PutAsync($"{apiUrl}/update/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new
                        {
                            success = true,
                            message = "Cập nhật câu hỏi thành công!",
                            redirectUrl = Url.Action("ManageReadingQuestion", "Admin")
                        });
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        return Json(new
                        {
                            success = false,
                            message = $"Cập nhật thất bại: {errorMessage}",
                            redirectUrl = Url.Action("ManageReadingQuestion", "Admin")
                        });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"Có lỗi xảy ra: {ex.Message}" });
                }
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }


        [HttpGet]
        public async Task<IActionResult> AddReadingQuestion()
        {
            // Lấy dữ liệu từ API (bất đồng bộ)
            var rankQuestions = await _httpClient.GetFromJsonAsync<IEnumerable<RankQuestionDto>>("https://localhost:7208/api/rankquestion/getall");

            var rankReadingQuestion = rankQuestions.Where(x => x.RankQuestionName.Contains("Đọc")).ToList();

            // Lưu vào ViewData
            ViewData["RankQuestions"] = rankReadingQuestion;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReadingQuestion(ReadingQuestionCreateDto createDto, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy chỉ số đáp án đúng từ form (radio button)
                    var correctAnswerIndexString = Request.Form["CorrectAnswerIndex"];
                    if (int.TryParse(correctAnswerIndexString, out int correctAnswerIndex))
                    {
                        for (int i = 0; i < createDto.ReadingAnswers.Count; i++)
                        {
                            createDto.ReadingAnswers[i].IsCorrect = (i == correctAnswerIndex);
                        }
                    }

                    // Tạo MultipartFormDataContent để gửi yêu cầu đến API
                    var content = new MultipartFormDataContent();

                    // Gửi ảnh nếu có
                    if (image != null)
                    {
                        var fileContent = new StreamContent(image.OpenReadStream());
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);
                        content.Add(fileContent, "image", image.FileName);
                    }

                    // Gửi các thuộc tính chính
                    content.Add(new StringContent(createDto.Question), "Question");
                    content.Add(new StringContent(createDto.RankQuestionId.ToString()), "RankQuestionId");
                    content.Add(new StringContent(createDto.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ss")), "CreatedDate");
                    content.Add(new StringContent(createDto.UpdatedDate.ToString("yyyy-MM-ddTHH:mm:ss")), "UpdatedDate");

                    // Gửi danh sách ReadingAnswers
                    for (int i = 0; i < createDto.ReadingAnswers.Count; i++)
                    {
                        var answer = createDto.ReadingAnswers[i];
                        content.Add(new StringContent(answer.Content), $"ReadingAnswers[{i}].Content");
                        content.Add(new StringContent(answer.IsCorrect.ToString().ToLower()), $"ReadingAnswers[{i}].IsCorrect");
                    }

                    // Gọi API để tạo mới câu hỏi
                    var response = await _httpClient.PostAsync($"{apiUrl}/add", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new
                        {
                            success = true,
                            message = "Tạo câu hỏi thành công!",
                            redirectUrl = Url.Action("ManageReadingQuestion", "Admin")
                        });
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        return Json(new
                        {
                            success = false,
                            message = $"Tạo câu hỏi thất bại: {errorMessage}",
                            redirectUrl = Url.Action("ManageReadingQuestion", "Admin")
                        });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"Có lỗi xảy ra: {ex.Message}" });
                }
            }

            return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatusReadingQuestion([FromBody] ReadingQuestionDto readingQuestionDto)
        {

            if (readingQuestionDto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ!");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWTToken"));
            if (readingQuestionDto.IsPublic == true)
            {
                readingQuestionDto.IsPublic = false;
            }
            else
            {
                readingQuestionDto.IsPublic = true;
            }
            var response = await _httpClient.PutAsJsonAsync(apiUrl, readingQuestionDto);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Thay đổi trạng thái câu hỏi thành công!" });
            }
            else
            {
                return Json(new { success = false, message = "Thay đổi trạng thái câu hỏi thất bại!" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Xóa câu hỏi đọc thành công !" });
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = $"Xóa thất bại: {error}" });
            }
        }

    }



}
