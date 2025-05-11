using DATN.Application.Dtos.ReadingDtos;
using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Interfaces
{
    public interface IReadingQuestionService
    {

        Task<IEnumerable<ReadingQuestion>> GetAllReadingQuestionsAsync();
        Task<ReadingQuestion> GetReadingQuestionByIdAsync(int id);
        Task<Result> CreateReadingQuestionAsync(ReadingQuestion readingQuestion);
        Task<Result> UpdateReadingQuestionAsync(ReadingQuestion readingQuestion);
        Task<Result> DeleteReadingQuestionAsync(int id);
    }
}
