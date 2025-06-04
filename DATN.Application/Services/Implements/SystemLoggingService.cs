using DATN.Application.Dtos.SystemLoggingDtos.Chart;
using DATN.Application.Services.Interfaces;
using DATN.Domain.Entities;
using DATN.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

namespace DATN.Application.Services.Implements
{
    public class SystemLoggingService : ISystemLoggingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SystemLoggingService> _logger;

        public SystemLoggingService(IUnitOfWork unitOfWork, ILogger<SystemLoggingService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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

        public async Task<IEnumerable<AccessStatsDto>> GetAccessStatsAsync(DateTime? fromDate, DateTime? toDate, string? action, string? ip, Guid? userId)
        {
            _logger.LogInformation($"GetAccessStatsAsync called with fromDate: {fromDate}, toDate: {toDate}");

            var query = _unitOfWork.SystemLoggingRepository.GetAll()
                .Where(x => x.ActionName.ToLower() == "Login - Success".ToLower());

            if (fromDate.HasValue)
            {
                _logger.LogInformation($"Applying fromDate filter: {fromDate.Value}");
                query = query.Where(x => x.CreatedDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                _logger.LogInformation($"Applying toDate filter: {toDate.Value}");
                query = query.Where(x => x.CreatedDate <= toDate.Value);
            }

            _logger.LogInformation($"Executing query...");

            var result = await query
                .GroupBy(x => x.CreatedDate.Date)
                .Select(g => new AccessStatsDto
                {
                    ActionName = "Login",
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            _logger.LogInformation($"Query executed. Found {result.Count} entries.");

            return result;
        }

        public async Task<IEnumerable<ChartDataDto>> GetDailyAccessChartAsync(DateTime? fromDate, DateTime? toDate)
        {
            _logger.LogInformation($"GetDailyAccessChartAsync called with fromDate: {fromDate}, toDate: {toDate}");

            var query = _unitOfWork.SystemLoggingRepository.GetAll()
                  .Where(x => x.ActionName.ToLower() == "Login - Success".ToLower());

            if (fromDate.HasValue)
            {
                _logger.LogInformation($"Applying fromDate filter: {fromDate.Value}");
                query = query.Where(x => x.CreatedDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                _logger.LogInformation($"Applying toDate filter: {toDate.Value}");
                query = query.Where(x => x.CreatedDate <= toDate.Value);
            }

            _logger.LogInformation($"Executing chart query...");

            var result = await query
                .GroupBy(x => x.CreatedDate.Date)
                .Select(g => new ChartDataDto
                {
                    Date = g.Key,
                    AccessCount = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

             _logger.LogInformation($"Chart query executed. Found {result.Count} entries.");

            return result;
        }

       




    }

}
