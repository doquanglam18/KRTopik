using DATN.Domain.Entities;
using DATN.Infrastructure.Context;
using DATN.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Repository.Implements
{
    public class ReadingQuestionRepository : GenericRepository<ReadingQuestion>, IReadingQuestionRepository
    {
        public ReadingQuestionRepository(DATNContext context) : base(context)
        {
        }

        public override IQueryable<ReadingQuestion> GetAll()
        {
            return _context.readingQuestion
                .Include(rq => rq.ReadingAnswers)
                .Include(rq => rq.TestSet)
                .Include(rq => rq.RankQuestion);
        }

        public override async Task<ReadingQuestion> GetByIdAsync(int id)
        {
            return await _context.readingQuestion
                .Include(rq => rq.ReadingAnswers)
                .Include(rq => rq.TestSet)
                .Include(rq => rq.RankQuestion)
                .FirstOrDefaultAsync(rq => rq.Id == id);
        }

        public async Task<ReadingQuestion> GetByIdWithAnswersAsync(int id)
        {
            return await _context.readingQuestion
                .Include(q => q.ReadingAnswers)
                .FirstOrDefaultAsync(q => q.Id == id);
        }


    }
}
