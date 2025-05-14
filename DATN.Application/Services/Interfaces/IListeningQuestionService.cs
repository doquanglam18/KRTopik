using DATN.Application.Dtos.BaseDtos;
using DATN.Application.Dtos.ListeningDtos;
using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Interfaces
{
    public interface IListeningQuestionService
    {
        Task<IEnumerable<ListeningQuestion>> GetAllListeningQuestionsAsync();
        Task<ListeningQuestion> GetListeningQuestionByIdAsync(int id);
        Task<Result> CreateListeningQuestionAsync(ListeningQuestion listeningQuestion);
        Task<Result> UpdateListeningQuestionAsync(ListeningQuestion listeningQuestion);
        Task<Result> DeleteListeningQuestionAsync(int id);
        Task<PageResultDto<ListeningQuestionDto>> GetAllListeningQuestionsPagingAsync(int page, int pageSize);
    }
}
