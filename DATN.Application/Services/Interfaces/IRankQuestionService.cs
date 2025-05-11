using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Interfaces
{
    public interface IRankQuestionService
    {
        Task<IEnumerable<RankQuestion>> GetAllRankAsync();
    }
}
