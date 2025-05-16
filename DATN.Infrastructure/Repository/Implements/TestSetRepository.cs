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
    public class TestSetRepository : GenericRepository<TestSet>, ITestSetRepository
    {
        public TestSetRepository(DATNContext context) : base(context)
        {
        }

        public override IQueryable<TestSet> GetAll()
        {
            return _context.testSet
                .Include(rq => rq.RankQuestion)
                .Include(rq => rq.ReadingQuestions)
                    .ThenInclude(rq => rq.ReadingAnswers)
                .Include(rq => rq.UserProgress)
                .Include(rq => rq.ListeningQuestions)
                    .ThenInclude(rq => rq.ListeningAnswers)
                .Include(rq => rq.Comments);
        }

        public override async Task<TestSet> GetByIdAsync(int id)
        {
            return await _context.testSet
                .Include(rq => rq.RankQuestion)
                .Include(rq => rq.ReadingQuestions)
                    .ThenInclude(rq => rq.ReadingAnswers)
                .Include(rq => rq.UserProgress)
                .Include(rq => rq.ListeningQuestions)
                    .ThenInclude(rq => rq.ListeningAnswers)
                .Include(rq => rq.Comments)
                .FirstOrDefaultAsync(rq => rq.Id == id);
        }

        public IQueryable<TestSet> GetAllForPaging()
        {
            return _context.Set<TestSet>();
        }
    }
}
