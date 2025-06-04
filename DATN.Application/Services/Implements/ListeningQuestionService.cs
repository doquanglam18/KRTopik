using AutoMapper;
using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Application.Dtos.ListeningDtos.ForAddTestSet;
using DATN.Application.Dtos.ReadingDtos;
using DATN.Application.Dtos.ReadingDtos.ForAddTestSet;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
    public class ListeningQuestionService : IListeningQuestionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ListeningQuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> CreateListeningQuestionAsync(ListeningQuestion listeningQuestion)
        {
            try
            {
                // Validate câu hỏi
                if (string.IsNullOrWhiteSpace(listeningQuestion.Question))
                    return Result.Failure("Nội dung câu hỏi nghe không được để trống.");

                if (listeningQuestion.RankQuestionId <= 0)
                    return Result.Failure("Câu hỏi nghe phải thuộc một cấp độ cụ thể.");

                if (listeningQuestion.ListeningAnswers == null || listeningQuestion.ListeningAnswers.Count < 4)
                    return Result.Failure("Phải có ít nhất 4 đáp án với mỗi câu hỏi.");

                if (listeningQuestion.ListeningAnswers.Count(a => a.IsCorrect) != 1)
                    return Result.Failure("Phải có đúng một đáp án đúng duy nhất.");

                if(listeningQuestion.ListeningSoundURL == null || listeningQuestion.ListeningSoundURL == string.Empty)
                    return Result.Failure("Bắt buộc phải có một file âm thanh cho câu hỏi nghe.");

                // Thiết lập liên kết ngược giữa Answer và Question
                foreach (var answer in listeningQuestion.ListeningAnswers)
                {
                    answer.ListeningQuestion = listeningQuestion; // optional nếu bạn dùng EF navigation
                }

                listeningQuestion.CreatedDate = DateTime.Now;

                // Add vào DB
                await _unitOfWork.ListenQuestionRepository.Add(listeningQuestion);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Tạo câu hỏi nghe thành công.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Có lỗi khi tạo câu hỏi nghe: {ex.Message}");
            }
        }

        public async Task<Result> DeleteListeningQuestionAsync(int id)
        {
            ListeningQuestion listeningQuestion = await GetListeningQuestionByIdAsync(id);
            try
            {
                if (listeningQuestion == null)
                {
                    return Result.Failure("Không tìm thấy câu hỏi muốn xóa.");
                }

                if (listeningQuestion.TestSet != null)
                {
                    return Result.Failure("Không thể xóa câu hỏi đang thuộc 1 đề ôn tập !");
                }


                // Xóa câu hỏi
                await _unitOfWork.ListenQuestionRepository.Delete(listeningQuestion);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Xóa câu hỏi nghe thành công !.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Đã xảy ra lỗi khi xóa câu hỏi nghe: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ListeningQuestion>> GetAllListeningQuestionsAsync()
        {
            var listeningQuestions = await _unitOfWork.ListenQuestionRepository
                .GetAll()
                .ToListAsync();
            return listeningQuestions;
        }

        public async Task<PageResultDto<ListeningQuestionDto>> GetAllListeningQuestionsPagingAsync(int page, int pageSize)
        {    
             var   query = _unitOfWork.ListenQuestionRepository.GetAllForPaging()
               .Include(u => u.RankQuestion)
               .Include(u => u.ListeningAnswers)
               .Include(u => u.TestSet);
            

            var totalItem = query.Count();

            var listeningQuestions = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PageResultDto<ListeningQuestionDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalItem = totalItem,
                Items = _mapper.Map<List<ListeningQuestionDto>>(listeningQuestions)
            };
        }
        public async Task<List<ListeningQsDto>> GetListeningByRankIDNoPagging(int rankQuestionId)
        {
            var listeningQuestions = await _unitOfWork.ListenQuestionRepository
                .GetAll()
                .Include(u => u.RankQuestion)
                .Include(u => u.ListeningAnswers)
                .Include(u => u.TestSet)
                .Where(u => u.RankQuestionId == rankQuestionId)
                .Where(u => u.IsPublic == true)
                .Where(u => u.TestSet == null)
                .ToListAsync();

            var listeningQuestionsDto = _mapper.Map<List<ListeningQsDto>>(listeningQuestions);
            return listeningQuestionsDto;
        }

        public async Task<PageResultDto<ListeningQsDto>> GetListeningByRankID(int rankQuestionId, int page, int pageSize, int testSetId)
        {
            try
            {
                Console.WriteLine($"Getting listening questions for rank {rankQuestionId}, page {page}, pageSize {pageSize}");
                
                var query = _unitOfWork.ListenQuestionRepository.GetAllForPaging()
                    .Include(u => u.RankQuestion)
                    .Include(u => u.ListeningAnswers)
                    .Include(u => u.TestSet)
                    .Where(u => u.RankQuestionId == rankQuestionId)
                    .Where(u => u.TestSetId == testSetId || u.TestSetId == null);

                var totalItem = query.Count();
                Console.WriteLine($"Total items in database: {totalItem}");

                if (totalItem == 0)
                {
                    Console.WriteLine("No questions found in database for this rank");
                    return new PageResultDto<ListeningQsDto>
                    {
                        Page = page,
                        PageSize = pageSize,
                        TotalItem = 0,
                        Items = new List<ListeningQsDto>()
                    };
                }

                var listeningQuestions = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                Console.WriteLine($"Retrieved {listeningQuestions.Count} questions from database");

                if (listeningQuestions.Count == 0)
                {
                    Console.WriteLine("No questions found after paging");
                    return new PageResultDto<ListeningQsDto>
                    {
                        Page = page,
                        PageSize = pageSize,
                        TotalItem = totalItem,
                        Items = new List<ListeningQsDto>()
                    };
                }

                var mappedItems = _mapper.Map<List<ListeningQsDto>>(listeningQuestions);
                Console.WriteLine($"Mapped {mappedItems?.Count ?? 0} items to DTOs");

                if (mappedItems == null || mappedItems.Count == 0)
                {
                    Console.WriteLine("Mapping resulted in no items");
                    return new PageResultDto<ListeningQsDto>
                    {
                        Page = page,
                        PageSize = pageSize,
                        TotalItem = totalItem,
                        Items = new List<ListeningQsDto>()
                    };
                }

                var result = new PageResultDto<ListeningQsDto>
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalItem = totalItem,
                    Items = mappedItems
                };

                Console.WriteLine($"Returning {result.Items.Count} items");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetListeningByRankID: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<ListeningQuestion> GetListeningQuestionByIdAsync(int id)
        {
            var listeningQuestion = await _unitOfWork.ListenQuestionRepository.GetByIdAsync(id);
            return listeningQuestion;
        }
        public async Task<Result> UpdateListeningQuestionAsyncByIdForNullTestSet(int questionId)
        {
            try
            {
                // Lấy câu hỏi và kiểm tra null
                var question = await GetListeningQuestionByIdAsync(questionId);
                if (question == null)
                {
                    return Result.Failure($"Không tìm thấy câu hỏi với ID: {questionId}");
                }

                // Kiểm tra xem câu hỏi có thuộc test set nào không
                if (question.TestSetId == null)
                {
                    return Result.Success("Câu hỏi đã không thuộc test set nào.");
                }

                // Cập nhật TestSetId thành null
                question.TestSetId = null;

                // Lưu thay đổi
                await _unitOfWork.ListenQuestionRepository.Update(question);
                var saveResult = await _unitOfWork.SaveChangesAsync();

                if (saveResult > 0)
                {
                    return Result.Success("Cập nhật câu hỏi thành công.");
                }
                else
                {
                    return Result.Failure("Không có thay đổi nào được lưu.");
                }
            }
            catch (Exception ex)
            {
                return Result.Failure($"Có lỗi xảy ra khi cập nhật câu hỏi: {ex.Message}");
            }
        }

        public async Task<Result> UpdateListeningQuestionAsync(ListeningQuestion existingQuestion)
        {
            try
            {
                // Validate nếu có vấn đề về dữ liệu
                if (string.IsNullOrWhiteSpace(existingQuestion.Question))
                    return Result.Failure("Nội dung câu hỏi không được để trống.");

                if (existingQuestion.RankQuestionId <= 0)
                    return Result.Failure("Câu hỏi phải thuộc một cấp độ cụ thể.");

                if (existingQuestion.ListeningAnswers == null || existingQuestion.ListeningAnswers.Count < 4)
                    return Result.Failure("Phải có ít nhất 4 đáp án với mỗi câu hỏi.");

                if (existingQuestion.ListeningAnswers.Count(a => a.IsCorrect) != 1)
                    return Result.Failure("Phải có đúng một đáp án đúng duy nhất.");

                if (string.IsNullOrWhiteSpace(existingQuestion.ListeningSoundURL))
                    return Result.Failure("Phải cung cấp một đường dẫn file âm thanh hợp lệ cho câu hỏi nghe.");


                // Lấy câu hỏi và đáp án hiện tại từ DB để tránh xung đột với EF
                var existing = await _unitOfWork.ListenQuestionRepository.GetByIdWithAnswersAsync(existingQuestion.Id);

                if (existing == null)
                    return Result.Failure("Không tìm thấy câu hỏi để cập nhật.");

                // Cập nhật các thông tin của câu hỏi
                existing.Question = existingQuestion.Question;
                existing.ListeningSoundURL = existingQuestion.ListeningSoundURL;
                existing.RankQuestionId = existingQuestion.RankQuestionId;
                existing.UpdatedDate = DateTime.Now;
                existing.UpdatedBy = existingQuestion.UpdatedBy;
                existing.IsPublic = existingQuestion.IsPublic;
                existing.ListeningScript = existingQuestion.ListeningScript;

                // Cập nhật các đáp án mới hoặc sửa các đáp án đã có
                var updatedAnswers = existingQuestion.ListeningAnswers;

                // Xóa các đáp án không có trong danh sách mới
                foreach (var existingAnswer in existing.ListeningAnswers.ToList())
                {
                    if (!updatedAnswers.Any(a => a.Id == existingAnswer.Id))
                    {
                        existing.ListeningAnswers.Remove(existingAnswer);
                    }
                }

                // Thêm hoặc cập nhật các đáp án
                foreach (var updatedAnswer in updatedAnswers)
                {
                    var existingAnswer = existing.ListeningAnswers
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
                        existing.ListeningAnswers.Add(new ListeningAnswer
                        {
                            Content = updatedAnswer.Content,
                            IsCorrect = updatedAnswer.IsCorrect,
                            ListeningQuestion = existing // liên kết lại với câu hỏi
                        });
                    }
                }

                // Chắc chắn rằng có ít nhất một đáp án đúng
                var correctAnswer = existing.ListeningAnswers.FirstOrDefault(a => a.IsCorrect);
                if (correctAnswer == null)
                {
                    return Result.Failure("Phải có ít nhất một đáp án đúng.");
                }

                // Lưu lại các thay đổi vào DB
                await _unitOfWork.ListenQuestionRepository.Update(existing);
                await _unitOfWork.SaveChangesAsync();

                return Result.Success("Cập nhật câu hỏi nghe thành công.");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Có lỗi khi cập nhật câu hỏi: {ex.Message}");
            }
        }
    }
}
