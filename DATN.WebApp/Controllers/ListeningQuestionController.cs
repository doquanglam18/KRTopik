using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.RankQuestionDtos;
using DATN.Application.Dtos.ReadingDtos;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace DATN.WebApp.Controllers
{
    public class ListeningQuestionController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://localhost:7208/api/listeningquestion";

        public ListeningQuestionController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public IActionResult UpdateListeningQuestion(ListeningQuestionDto listeningQuestionDto)
        {
            return View(listeningQuestionDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateListeningQuestion(int id, ListeningQuestionDto updateDto, IFormFile? sound)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy chỉ số đáp án đúng từ form (radio button)
                    var correctAnswerIndexString = Request.Form["CorrectAnswerIndex"];
                    if (int.TryParse(correctAnswerIndexString, out int correctAnswerIndex))
                    {
                        for (int i = 0; i < updateDto.ListeningAnswers.Count; i++)
                        {
                            updateDto.ListeningAnswers[i].IsCorrect = (i == correctAnswerIndex);
                        }
                    }

                    // Tạo MultipartFormDataContent để gửi yêu cầu đến API
                    var content = new MultipartFormDataContent();

                    // Gửi sound nếu có
                    if (sound != null)
                    {
                        var fileContent = new StreamContent(sound.OpenReadStream());
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(sound.ContentType);
                        content.Add(fileContent, "sound", sound.FileName);
                    }

                    // Gửi các thuộc tính chính
                    content.Add(new StringContent(updateDto.Id.ToString()), "Id");

                    content.Add(new StringContent(updateDto.Question), "Question");
                    content.Add(new StringContent(updateDto.TestSetId.ToString()), "TestSetId");
                    content.Add(new StringContent(updateDto.RankQuestionId.ToString()), "RankQuestionId");
                    content.Add(new StringContent(updateDto.ListeningSoundURL ?? string.Empty), "ListeningSoundURL");
                    content.Add(new StringContent(updateDto.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ss")), "CreatedDate");
                    content.Add(new StringContent(updateDto.UpdatedDate.ToString("yyyy-MM-ddTHH:mm:ss")), "UpdatedDate");
                    content.Add(new StringContent(updateDto.ListeningScript ?? string.Empty), "ListeningScript");

                    content.Add(new StringContent(updateDto.IsPublic.ToString()), "IsPublic");

                    for (int i = 0; i < updateDto.ListeningAnswers.Count; i++)
                    {
                        var answer = updateDto.ListeningAnswers[i];

                        content.Add(new StringContent(answer.Id.ToString()), $"ListeningAnswers[{i}].Id");
                        content.Add(new StringContent(answer.Content), $"ListeningAnswers[{i}].Content");
                        content.Add(new StringContent(answer.IsCorrect.ToString().ToLower()), $"ListeningAnswers[{i}].IsCorrect");

                        // THÊM ĐOẠN NÀY ĐỂ GỬI FILE ẢNH (nếu có)
                        if (answer.Image != null)
                        {
                            var imageContent = new StreamContent(answer.Image.OpenReadStream());
                            imageContent.Headers.ContentType = new MediaTypeHeaderValue(answer.Image.ContentType);
                            content.Add(imageContent, $"ListeningAnswers[{i}].Image", answer.Image.FileName);
                        }
                    }


                    // Gọi API cập nhật câu hỏi
                    var response = await _httpClient.PutAsync($"{apiUrl}/update/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new
                        {
                            success = true,
                            message = "Cập nhật câu hỏi nghe thành công!",
                            redirectUrl = Url.Action("ManageListeningQuestion", "Admin")
                        });
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        return Json(new
                        {
                            success = false,
                            message = $"Cập nhật thất bại: {errorMessage}",
                            redirectUrl = Url.Action("ManageListeningQuestion", "Admin")
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
        public async Task<IActionResult> AddListeningQuestion()
        {
            // Lấy dữ liệu từ API (bất đồng bộ)
            var rankQuestions = await _httpClient.GetFromJsonAsync<IEnumerable<RankQuestionDto>>("https://localhost:7208/api/rankquestion/getall");

            var rankReadingQuestion = rankQuestions.Where(x => x.RankQuestionName.Contains("Nghe")).ToList();

            // Lưu vào ViewData
            ViewData["RankQuestions"] = rankReadingQuestion;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddListeningQuestion(ListeningQuestionCreateDto createDto, IFormFile? sound)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy chỉ số đáp án đúng từ form (radio button)
                    var correctAnswerIndexString = Request.Form["CorrectAnswerIndex"];
                    if (int.TryParse(correctAnswerIndexString, out int correctAnswerIndex))
                    {
                        for (int i = 0; i < createDto.ListeningAnswers.Count; i++)
                        {
                            createDto.ListeningAnswers[i].IsCorrect = (i == correctAnswerIndex);
                        }
                    }

                    // Tạo MultipartFormDataContent để gửi yêu cầu đến API
                    var content = new MultipartFormDataContent();

                    // Gửi sound nếu có
                    if (sound != null)
                    {
                        var fileContent = new StreamContent(sound.OpenReadStream());
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(sound.ContentType);
                        content.Add(fileContent, "sound", sound.FileName);
                    }

                    // Gửi các thuộc tính chính
                    content.Add(new StringContent(createDto.Question), "Question");
                    content.Add(new StringContent(createDto.RankQuestionId.ToString()), "RankQuestionId");
                    content.Add(new StringContent(createDto.CreatedDate.ToString("yyyy-MM-ddTHH:mm:ss")), "CreatedDate");
                    content.Add(new StringContent(createDto.UpdatedDate.ToString("yyyy-MM-ddTHH:mm:ss")), "UpdatedDate");
                    content.Add(new StringContent(createDto.ListeningScript.ToString()), "ListeningScript");

                    // Gửi danh sách ReadingAnswers
                    for (int i = 0; i < createDto.ListeningAnswers.Count; i++)
                    {
                        var answer = createDto.ListeningAnswers[i];

                        if(answer.Content == null)
                        {
                            answer.Content = string.Empty;
                        }
                        content.Add(new StringContent(answer.Content), $"ListeningAnswers[{i}].Content");
                        content.Add(new StringContent(answer.IsCorrect.ToString().ToLower()), $"ListeningAnswers[{i}].IsCorrect");

                        // GỬI FILE ẢNH nếu có
                        if (answer.Image != null)
                        {
                            var imageContent = new StreamContent(answer.Image.OpenReadStream());
                            imageContent.Headers.ContentType = new MediaTypeHeaderValue(answer.Image.ContentType);
                            content.Add(imageContent, $"ListeningAnswers[{i}].Image", answer.Image.FileName);
                        }
                    }


                    // Gọi API để tạo mới câu hỏi
                    var response = await _httpClient.PostAsync($"{apiUrl}/add", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new
                        {
                            success = true,
                            message = "Tạo câu hỏi nghe thành công!",
                            redirectUrl = Url.Action("ManageListeningQuestion", "Admin")
                        });
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        return Json(new
                        {
                            success = false,
                            message = $"Tạo câu hỏi thất bại: {errorMessage}",
                            redirectUrl = Url.Action("ManageListeningQuestion", "Admin")
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
                return Json(new { success = true, message = "Xóa câu hỏi nghe thành công !" });
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = $"Xóa thất bại: {error}" });
            }
        }

    }

}
