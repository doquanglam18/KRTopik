using DATN.Domain.Entities;
using DATN.Infrastructure.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProgress = DATN.Domain.Entities.UserProgress;

namespace DATN.Infrastructure.Repository.Interfaces
{
    public interface IUserProgressRepository : IGenericRepository<UserProgress>
    {
    }
}
