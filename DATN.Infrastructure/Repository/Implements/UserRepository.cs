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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DATNContext context) : base(context)
        {
        }

        public override IQueryable<User> GetAll()
        {
            return _context.user
                .Include(u => u.Role)
                .Include(u => u.SystemLoggings)
                .Include(u => u.RatingBlogs)
                .Include(u => u.UserProgresses)
                .Include(u => u.Comments);
        }
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.user
                 .Include(u => u.Role)
                .Include(u => u.SystemLoggings)
                .Include(u => u.RatingBlogs)
                .Include(u => u.UserProgresses)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User> GetUserForLogin(string email, string passWord)
        {
            var user = await _context.user
                .Include(u => u.Role)
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user != null && BCrypt.Net.BCrypt.Verify(passWord, user.PasswordHash))
            {
                return user;
            }

            return null;
        }

    }
}
