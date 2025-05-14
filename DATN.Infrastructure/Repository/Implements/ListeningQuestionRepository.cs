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
    public class ListeningQuestionRepository : GenericRepository<ListeningQuestion>, IListeningQuestionRepository
    {
        public ListeningQuestionRepository(DATNContext context) : base(context)
        {
        }

        public override IQueryable<ListeningQuestion> GetAll()
        {
            return _context.listenQuestion
                .Include(rq => rq.ListeningAnswers)
                .Include(rq => rq.TestSet)
                .Include(rq => rq.RankQuestion);
        }

        public override async Task<ListeningQuestion> GetByIdAsync(int id)
        {
            return await _context.listenQuestion
                .Include(rq => rq.ListeningAnswers)
                .Include(rq => rq.TestSet)
                .Include(rq => rq.RankQuestion)
                .FirstOrDefaultAsync(rq => rq.Id == id);
        }
        public async Task<ListeningQuestion> GetByIdWithAnswersAsync(int id)
        {
            return await _context.listenQuestion
                .Include(q => q.ListeningAnswers)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public IQueryable<ListeningQuestion> GetAllForPaging()
        {
            return _context.Set<ListeningQuestion>();
        }
    }
}
