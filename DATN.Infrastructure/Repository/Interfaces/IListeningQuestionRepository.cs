using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Repository.Interfaces
{
    public interface IListeningQuestionRepository : IGenericRepository<ListeningQuestion>
    {
        Task<ListeningQuestion> GetByIdWithAnswersAsync(int id);
        IQueryable<ListeningQuestion> GetAllForPaging();
    }
}
