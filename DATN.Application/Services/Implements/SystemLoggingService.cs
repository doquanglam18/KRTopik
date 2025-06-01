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

        public async Task<IEnumerable<AccessStatsDto>> GetAccessStatsAsync(DateTime? fromDate, DateTime? toDate, string? action, string? ip, Guid? userId)
        {
            var query = _unitOfWork.SystemLoggingRepository.GetAll();

            if (fromDate.HasValue)
                query = query.Where(x => x.CreatedDate >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(x => x.CreatedDate <= toDate.Value);
            if (!string.IsNullOrEmpty(action))
                query = query.Where(x => x.ActionName == action);
            if (!string.IsNullOrEmpty(ip))
                query = query.Where(x => x.IPAddress == ip);
            if (userId.HasValue)
                query = query.Where(x => x.UserId == userId);

            var result = await query
                .GroupBy(x => new { x.ActionName, x.IPAddress, x.UserId })
                .Select(g => new AccessStatsDto
                {
                    ActionName = g.Key.ActionName,
                    IPAddress = g.Key.IPAddress,
                    UserId = g.Key.UserId,
                    Count = g.Count()
                })
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<ChartDataDto>> GetDailyAccessChartAsync(DateTime? fromDate, DateTime? toDate)
        {
            var query = _unitOfWork.SystemLoggingRepository.GetAll();

            if (fromDate.HasValue)
                query = query.Where(x => x.CreatedDate >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(x => x.CreatedDate <= toDate.Value);

            var result = await query
                .GroupBy(x => x.CreatedDate.Date)
                .Select(g => new ChartDataDto
                {
                    Date = g.Key,
                    AccessCount = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            return result;
        }

        public async Task<FileResult> ExportToExcelAsync(DateTime? fromDate, DateTime? toDate, string? action, string? ip, Guid? userId)
        {
            var data = await GetAccessStatsAsync(fromDate, toDate, action, ip, userId);

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Access Statistics");

            worksheet.Cells[1, 1].Value = "Action Name";
            worksheet.Cells[1, 2].Value = "IP Address";
            worksheet.Cells[1, 3].Value = "User ID";
            worksheet.Cells[1, 4].Value = "Count";

            int row = 2;
            foreach (var item in data)
            {
                worksheet.Cells[row, 1].Value = item.ActionName;
                worksheet.Cells[row, 2].Value = item.IPAddress;
                worksheet.Cells[row, 3].Value = item.UserId?.ToString();
                worksheet.Cells[row, 4].Value = item.Count;
                row++;
            }

            var content = package.GetAsByteArray();
            return new FileResult
            {
                Content = content,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileName = $"AccessStats_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
            };
        }

        [Obsolete]
        public async Task<FileResult> ExportToPdfAsync(DateTime? fromDate, DateTime? toDate, string? action, string? ip, Guid? userId)
        {
            var data = await GetAccessStatsAsync(fromDate, toDate, action, ip, userId);

            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12);
            int y = 40;
            gfx.DrawString("Access Statistics", new XFont("Verdana", 16), XBrushes.Black, new XRect(0, y, page.Width, page.Height), XStringFormats.TopCenter);
            y += 30;
            foreach (var item in data)
            {
                string line = $"Action: {item.ActionName}, IP: {item.IPAddress}, UserId: {item.UserId}, Count: {item.Count}";
                gfx.DrawString(line, font, XBrushes.Black, new XRect(40, y, page.Width - 80, page.Height), XStringFormats.TopLeft);
                y += 20;

                if (y > page.Height - 40)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = 40;
                }
            }

            using var stream = new MemoryStream();
            document.Save(stream, false);
            return new FileResult
            {
                Content = stream.ToArray(),
                ContentType = "application/pdf",
                FileName = $"AccessStats_{DateTime.Now:yyyyMMddHHmmss}.pdf"
            };
        }




    }

}
