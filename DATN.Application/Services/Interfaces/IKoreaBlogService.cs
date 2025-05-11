using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Interfaces
{
    public interface IKoreaBlogService
    {
        Task<IEnumerable<KoreaBlog>> GetAllKoreaBlogAsync();
        Task<Result> AddKoreaBlogAsync(KoreaBlog koreaBlog);
        Task<Result> DeleteKoreaBlogAsync(int id);
        Task<Result> UpdateKoreaBlogAsync(KoreaBlog koreaBlog);
        Task<KoreaBlog> GetKoreaBlogByIdAsync(int id);
    }
}
