using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Implements
{
    public class RankQuestionService : IRankQuestionService
    {

        protected readonly IUnitOfWork _unitOfWork;
        public RankQuestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<RankQuestion>> GetAllRankAsync()
        {
            var rankQuestions = await _unitOfWork.RankQuestionRepository.GetAll().ToListAsync();
            return rankQuestions;
        }
    }
}
