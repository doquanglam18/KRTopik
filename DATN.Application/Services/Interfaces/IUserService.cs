using DATN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<Result> AddUserAsync(User user);
        Task<Result> DeleteUserAsync(Guid id);
        Task<Result> UpdateUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid id);

        Task<ResultV<User>> CheckLogin(string email, string passWord);
    }
}
