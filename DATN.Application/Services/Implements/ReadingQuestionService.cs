using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.UnitOfWork;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
    public class ReadingQuestionService : IReadingQuestionService
    {

        private readonly IUnitOfWork _unitOfWork;
        public ReadingQuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> CreateReadingQuestionAsync(ReadingQuestion readingQuestion)
        {
            try
            {
                // Validate câu hỏi
                if (string.IsNullOrWhiteSpace(readingQuestion.Question))
                    return Result.Failure("Nội dung câu hỏi không được để trống.");

                if (readingQuestion.RankQuestionId <= 0)
                    return Result.Failure("Câu hỏi phải thuộc một cấp độ cụ thể.");

                if (readingQuestion.ReadingAnswers == null || readingQuestion.ReadingAnswers.Count < 4)
                    return Result.Failure("Phải có ít nhất 4 đáp án với mỗi câu hỏi.");

                if (readingQuestion.ReadingAnswers.Count(a => a.IsCorrect) != 1)
                    return Result.Failure("Phải có đúng một đáp án đúng duy nhất.");

                // Thiết lập liên kết ngược giữa Answer và Question
                foreach (var answer in readingQuestion.ReadingAnswers)
                {
                    answer.ReadingQuestion = readingQuestion; // optional nếu bạn dùng EF navigation
                }

                readingQuestion.CreatedDate = DateTime.Now;

                // Add vào DB
                await _unitOfWork.ReadingQuestionRepository.Add(readingQuestion);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Tạo câu hỏi thành công.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Có lỗi khi tạo câu hỏi: {ex.Message}");
            }
        }




        public async Task<IEnumerable<ReadingQuestion>> GetAllReadingQuestionsAsync()
        {
            var readingQuestions = await _unitOfWork.ReadingQuestionRepository
                .GetAll()
                .ToListAsync();
            return readingQuestions;
        }

        public async Task<ReadingQuestion> GetReadingQuestionByIdAsync(int id)
        {
            var readingQuestion = await _unitOfWork.ReadingQuestionRepository.GetByIdAsync(id);
            return readingQuestion;
        }

        /* public async Task<Result> UpdateReadingQuestionAsync(ReadingQuestion readingQuestion)
         {
             try
             {
                 // Validate
                 if (string.IsNullOrWhiteSpace(readingQuestion.Question))
                     return Result.Failure("Nội dung câu hỏi không được để trống.");

                 if (readingQuestion.RankQuestionId <= 0)
                     return Result.Failure("Câu hỏi phải thuộc một cấp độ cụ thể.");

                 if (readingQuestion.ReadingAnswers == null || readingQuestion.ReadingAnswers.Count < 4)
                     return Result.Failure("Phải có ít nhất 4 đáp án với mỗi câu hỏi.");

                 if (readingQuestion.ReadingAnswers.Count(a => a.IsCorrect) != 1)
                     return Result.Failure("Phải có đúng một đáp án đúng duy nhất.");

                 // Lấy entity gốc từ DB để tránh EF bị conflict tracking
                 var existing = await _unitOfWork.ReadingQuestionRepository
                     .GetByIdWithAnswersAsync(readingQuestion.Id); // bạn cần thêm hàm này

                 if (existing == null)
                     return Result.Failure("Không tìm thấy câu hỏi để cập nhật.");

                 // Cập nhật thủ công các field
                 existing.Question = readingQuestion.Question;
                 existing.ReadingImageURL = readingQuestion.ReadingImageURL;
                 existing.RankQuestionId = readingQuestion.RankQuestionId;
                 existing.UpdatedDate = DateTime.Now;
                 existing.UpdatedBy = readingQuestion.UpdatedBy;

                 // Cập nhật đáp án
                 var updatedAnswers = readingQuestion.ReadingAnswers;

                 // Cập nhật hoặc thêm mới các đáp án
                 foreach (var updatedAns in updatedAnswers)
                 {
                     var existingAns = existing.ReadingAnswers.FirstOrDefault(a => a.Id == updatedAns.Id);

                     if (existingAns != null)
                     {
                         // Cập nhật đáp án cũ
                         existingAns.Content = updatedAns.Content;
                         existingAns.IsCorrect = updatedAns.IsCorrect;
                     }
                     else
                     {
                         // Thêm đáp án mới
                         existing.ReadingAnswers.Add(new ReadingAnswer
                         {
                             Content = updatedAns.Content,
                             IsCorrect = updatedAns.IsCorrect,
                             ReadingQuestion = existing // liên kết lại với câu hỏi
                         });
                     }
                 }




                 // Update và save
                 await _unitOfWork.ReadingQuestionRepository.Update(existing);
                 await _unitOfWork.SaveChangesAsync();

                 return Result.Success("Cập nhật câu hỏi thành công.");
             }
             catch (Exception ex)
             {
                 return Result.Failure($"Có lỗi khi cập nhật câu hỏi: {ex.Message}");
             }
         }*/


        public async Task<Result> UpdateReadingQuestionAsync(ReadingQuestion existingQuestion)
        {
            try
            {
                // Validate nếu có vấn đề về dữ liệu
                if (string.IsNullOrWhiteSpace(existingQuestion.Question))
                    return Result.Failure("Nội dung câu hỏi không được để trống.");

                if (existingQuestion.RankQuestionId <= 0)
                    return Result.Failure("Câu hỏi phải thuộc một cấp độ cụ thể.");

                if (existingQuestion.ReadingAnswers == null || existingQuestion.ReadingAnswers.Count < 4)
                    return Result.Failure("Phải có ít nhất 4 đáp án với mỗi câu hỏi.");

                if (existingQuestion.ReadingAnswers.Count(a => a.IsCorrect) != 1)
                    return Result.Failure("Phải có đúng một đáp án đúng duy nhất.");

                // Lấy câu hỏi và đáp án hiện tại từ DB để tránh xung đột với EF
                var existing = await _unitOfWork.ReadingQuestionRepository.GetByIdWithAnswersAsync(existingQuestion.Id);

                if (existing == null)
                    return Result.Failure("Không tìm thấy câu hỏi để cập nhật.");

                // Cập nhật các thông tin của câu hỏi
                existing.Question = existingQuestion.Question;
                existing.ReadingImageURL = existingQuestion.ReadingImageURL;
                existing.RankQuestionId = existingQuestion.RankQuestionId;
                existing.UpdatedDate = DateTime.Now;
                existing.UpdatedBy = existingQuestion.UpdatedBy;
                existing.IsPublic = existingQuestion.IsPublic;

                // Cập nhật các đáp án mới hoặc sửa các đáp án đã có
                var updatedAnswers = existingQuestion.ReadingAnswers;

                // Xóa các đáp án không có trong danh sách mới
                foreach (var existingAnswer in existing.ReadingAnswers.ToList())
                {
                    if (!updatedAnswers.Any(a => a.Id == existingAnswer.Id))
                    {
                        existing.ReadingAnswers.Remove(existingAnswer);
                    }
                }

                // Thêm hoặc cập nhật các đáp án
                foreach (var updatedAnswer in updatedAnswers)
                {
                    var existingAnswer = existing.ReadingAnswers
                        .FirstOrDefault(a => a.Id == updatedAnswer.Id);

                    if (existingAnswer != null)
                    {
                        // Cập nhật đáp án cũ
                        existingAnswer.Content = updatedAnswer.Content;
                        existingAnswer.IsCorrect = updatedAnswer.IsCorrect;
                    }
                    else
                    {
                        // Thêm đáp án mới
                        existing.ReadingAnswers.Add(new ReadingAnswer
                        {
                            Content = updatedAnswer.Content,
                            IsCorrect = updatedAnswer.IsCorrect,
                            ReadingQuestion = existing // liên kết lại với câu hỏi
                        });
                    }
                }

                // Chắc chắn rằng có ít nhất một đáp án đúng
                var correctAnswer = existing.ReadingAnswers.FirstOrDefault(a => a.IsCorrect);
                if (correctAnswer == null)
                {
                    return Result.Failure("Phải có ít nhất một đáp án đúng.");
                }

                // Lưu lại các thay đổi vào DB
                await _unitOfWork.ReadingQuestionRepository.Update(existing);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Cập nhật câu hỏi thành công.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Có lỗi khi cập nhật câu hỏi: {ex.Message}");
            }
        }




        public async Task<Result> DeleteReadingQuestionAsync(int id)
        {
            ReadingQuestion readingQuestion = await GetReadingQuestionByIdAsync(id);
            try
            {
                if (readingQuestion == null)
                {
                    return Result.Failure("Không tìm thấy câu hỏi muốn xóa.");
                }

                if(readingQuestion.TestSet != null)
                {
                    return Result.Failure("Không thể xóa câu hỏi đang thuộc 1 đề ôn tập !");
                }

                // Xóa câu hỏi
                await _unitOfWork.ReadingQuestionRepository.Delete(readingQuestion);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Xóa câu hỏi đọc thành công !.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Đã xảy ra lỗi khi xóa câu hỏi: {ex.Message}");
            }
        }
    }
}
