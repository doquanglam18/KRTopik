using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.SystemLoggingDtos.Chart
{
    public class AccessStatsDto
    {
        public string ActionName { get; set; }
        public string IPAddress { get; set; }
        public Guid? UserId { get; set; }
        public int Count { get; set; }
    }
}
