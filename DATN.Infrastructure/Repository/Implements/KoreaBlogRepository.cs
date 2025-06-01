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
    public class KoreaBlogRepository : GenericRepository<KoreaBlog>, IKoreaBlogRepository
    {
        public KoreaBlogRepository(DATNContext context) : base(context)
        {
        }
        public override IQueryable<KoreaBlog> GetAll()
        {
            return _context.koreaBlog
                .Include(rq => rq.RatingBlogs);
                
        }

        public override async Task<KoreaBlog> GetByIdAsync(int id)
        {
            return await _context.koreaBlog
                .Include(rq => rq.RatingBlogs)
                .FirstOrDefaultAsync(rq => rq.Id == id);
        }

        public IQueryable<KoreaBlog> GetAllForPaging()
        {
            return _context.Set<KoreaBlog>();
        }
    }
}
