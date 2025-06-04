using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Application.Dtos.SystemLoggingDtos.Chart
{
    public class AccessStatsViewModel
    {
        public List<AccessStatsDto> Stats { get; set; }
        public List<ChartDataDto> ChartData { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

}
