using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetUserForLogin(string email, string passWord);

        Task<User> GetUserByIdAsync(Guid id);


    }
}
