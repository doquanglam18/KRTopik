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
    public class SystemLoggingService : ISystemLoggingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SystemLoggingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SystemLogging>> GetAllSystemLoggingAsync()
        {
            var systemLoggings = await _unitOfWork.SystemLoggingRepository.GetAll().ToListAsync();
            return systemLoggings;
        }

        public async Task LogAction(Guid? userId, string ipAddress, string actionName, string details)
        {
            var log = new SystemLogging
            {
                UserId = userId,
                IPAddress = ipAddress,
                ActionName = actionName,
                Details = details,
                CreatedDate = DateTime.UtcNow
            };

            await _unitOfWork.SystemLoggingRepository.Add(log);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
